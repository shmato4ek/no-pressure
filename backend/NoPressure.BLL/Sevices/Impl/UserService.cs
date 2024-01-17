﻿using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
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

        public UserService(IUnitOfWork ouw, IMapper mapper, IStatisticService statisticService)
        {
            _uow = ouw;
            _mapper = mapper;
            _statisticService = statisticService;
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
            
            _uow.UserRepository.Create(userEntity);

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

            return _mapper.Map<UserInfo>(foundUser);
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
                    Date = follower.Date.ToString("MM/dd/yyyy")
                });
            }

            foreach (var following in followings)
            {
                subscriptions.Followings.Add(new UserSubscription()
                {
                    User = _mapper.Map<UserInfo>(following.Following),
                    Date = following.Date.ToString("MM/dd/yyyy")
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
                throw new Exception($"There is no user with id {following}");
            }

            var subscription = new Subscription()
            {
                FollowerId = followerId,
                FollowingId = followingId
            };

            _uow.SubscriptionRepository.Create(subscription);
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
