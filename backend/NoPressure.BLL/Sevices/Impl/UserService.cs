using AutoMapper;
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

        public UserService(IUnitOfWork ouw, IMapper mapper)
        {
            _uow = ouw;
            _mapper = mapper;
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

        public async Task<Subscriptions> GetUserSubscriptions(int userId)
        {
            var subscriptions = new Subscriptions();

            var followers = await _uow.SubscriptionRepository.GetAllUsersFollowers(userId);
            var followings = await _uow.SubscriptionRepository.GetAllUsersFollowings(userId);

            foreach (var follower in followers)
            {
                subscriptions.Followers.Add(new UserSubscription()
                {
                    User = _mapper.Map<UserInfo>(follower.Follower),
                    Date = follower.Date
                });
            }

            foreach (var following in followings)
            {
                subscriptions.Followings.Add(new UserSubscription()
                {
                    User = _mapper.Map<UserInfo>(following.Following),
                    Date = following.Date
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
    }
}
