using AutoMapper;
using NoPressure.BLL.Helpers;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.User;
using NoPressure.Common.Security;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.BLL.Sevices.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IStatisticService _statisticService;
        private readonly INotificationService _notificationService;

        public UserService(IUnitOfWork ouw, IMapper mapper, IStatisticService statisticService, INotificationService notificationService)
        {
            _uow = ouw;
            _mapper = mapper;
            _statisticService = statisticService;
            _notificationService = notificationService;
        }

        public async Task<UserDTO> CreateUser(NewUser newUser)
        {
            var isUserExist = await _uow.UserRepository.FindUserByEmail(newUser.Email);
            
            if (isUserExist != null)
            {
                throw new Exception();
            }

            var salt = SecurityHelper.GetRandomBytes();

            var userEntity = _mapper.Map<User>(newUser);
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(newUser.Password, salt);
            userEntity.RegistrationDate = DateTime.UtcNow;
            
            _uow.UserRepository.Create(userEntity);

            var settings = new Settings()
            {
                UserId = userEntity.Id,
                Statistic = SettingsPrivacy.AllUsers,
                Activities = SettingsPrivacy.AllUsers
            };

            _uow.SettingsRepository.Create(settings);

            await _uow.SaveAsync();

            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<UserInfo> GetUserById(int id)
        {
            var foundUser = await _uow.UserRepository.GetAllInfoById(id);

            if (foundUser is null) 
            {
                throw new Exception();
            }

            var userTeams = await _uow.TeamRepository.GetUsersTeams(id);
            foundUser.Teams = userTeams;

            var userInfo = _mapper.Map<UserInfo>(foundUser);

            if (userInfo.Teams != null)
            {
                foreach(var team in userInfo.Teams)
                {
                    var userSettings = userTeams
                        .FirstOrDefault(t => t.Id == team.Id)
                        .Settings
                        .FirstOrDefault(s => s.UserId == id);
                    
                    team.AddingActivities = userSettings.AddingActivities;
                }
            }

            userInfo.RegistrationDate = foundUser.RegistrationDate.ToString("dd/MM/yy");

            userInfo.IsNotificationsChecked = await _notificationService.CheckNotifications(userInfo.Id);

            return userInfo;
        }

        public async Task<UserShared> GetUserByEmail(string email, int userId)
        {
            var userShared = new UserShared();

            var encodedEmail = System.Convert.FromBase64String(email);
            var decodedEmail = System.Text.Encoding.UTF8.GetString(encodedEmail);

            var userEntity = await _uow
                .UserRepository
                .FindUserByEmail(decodedEmail);
            
            if (userEntity is null)
            {
                throw new Exception($"User with email {decodedEmail} was not founded.");
            }

            userShared.User = _mapper.Map<UserInfo>(userEntity);

            var followers = await _uow.SubscriptionRepository.GetAllUsersFollowers(userEntity.Id);

            var isFollowed = false;

            foreach (var follower in followers)
            {
                if(follower.FollowerId == userId)
                {
                    isFollowed = true;
                }
            }

            userShared.IsFollowed = isFollowed;

            userShared.Statistic = await _statisticService.GetActivitiesStatistic(userEntity.Id);
            
            return userShared;
        }

        public async Task<Subscriptions> GetUserSubscriptions(int userId)
        {
            var subscriptions = new Subscriptions()
            {
                Followers = new List<UserSubscription>(),
                Followings = new List<UserSubscription>()
            };

            var followers = await _uow.SubscriptionRepository.GetAllUsersFollowers(userId);
            var followings = await _uow.SubscriptionRepository.GetAllUsersFollowings(userId);

            foreach (var follower in followers)
            {
                subscriptions.Followers.Add(new UserSubscription()
                {
                    User = _mapper.Map<UserInfo>(follower.Follower),
                    Date = follower.Date.ToLocalTime().ToString("MM/dd/yyyy")
                });
            }

            foreach (var following in followings)
            {
                subscriptions.Followings.Add(new UserSubscription()
                {
                    User = _mapper.Map<UserInfo>(following.Following),
                    Date = following.Date.ToLocalTime().ToString("MM/dd/yyyy")
                });
            }

            return subscriptions;
        }

        public async Task Subscribe(int followerId, int followingId)
        {
            var follower = await _uow.UserRepository.FindAsync(followerId);

            if (follower is null)
            {
                throw new Exception($"There is no user with id {followerId}");
            }

            var following = await _uow.UserRepository.FindAsync(followingId);

            if (following is null)
            {
                throw new Exception($"There is no user with id {followingId}");
            }

            var subscription = new Subscription()
            {
                FollowerId = followerId,
                FollowingId = followingId,
                Date = DateTime.UtcNow
            };

            _uow.SubscriptionRepository.Create(subscription);

            var newNotification = new Notification()
            {
                UserId = followingId,
                Title = NotificationTitle.NewSubscription,
                Data = new NotificationData()
                {
                    SecondUserName = follower.Name,
                    Link = NotificationLinkHelper.GetNewSubscriptionLink(follower.Email),
                },
                Date = DateTime.UtcNow
            };

            _uow.NotificationRepository.Create(newNotification);

            await _uow.SaveAsync();
        }

        public async Task UnSubscribe(int followerId, int followingId)
        {
            var follower = await _uow.UserRepository.FindAsync(followerId);

            if (follower is null)
            {
                throw new Exception($"There is no user with id {followerId}");
            }

            var following = await _uow.UserRepository.FindAsync(followingId);

            if (following is null)
            {
                throw new Exception($"There is no user with id {following}");
            }

            await _uow.SubscriptionRepository.UnSubscribe(followerId, followingId);
            
            await _uow.SaveAsync();
        }

        public async Task<SettingsDTO> GetUserSettings(int userId)
        {
            var settings = await _uow.SettingsRepository.FindAsync(userId);

            return _mapper.Map<SettingsDTO>(settings);
        }

        public async Task UpdateUserSettings(SettingsDTO settings, int userId)
        {
            var settingsEntity = await _uow
                .SettingsRepository
                .FindSettingByUserId(userId);

            settingsEntity.Activities = settings.Activities;
            settingsEntity.Statistic = settings.Statistic;

            _uow.SettingsRepository.Update(settingsEntity);
        }

        public async Task UpdateUser(UpdateUser user)
        {
            var userEntity = await _uow.UserRepository.FindAsync(user.Id);

            userEntity.Email = user.Email;
            userEntity.Name = user.Name;

            _uow.UserRepository.Update(userEntity);
        }

        public async Task<UserInfo> ChangePassword(ChangePassword changePassword)
        {
            var userEntity = await _uow.UserRepository.FindAsync(changePassword.UserId);

            if(!SecurityHelper.IsValidPassword(userEntity.Password, changePassword.OldPassword, userEntity.Salt))
            {
                throw new Exception("Password is not valid");
            }

            var salt = SecurityHelper.GetRandomBytes();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(changePassword.NewPassword, salt);

            _uow.UserRepository.Update(userEntity);

            return _mapper.Map<UserInfo>(userEntity);
        }
    }
}
