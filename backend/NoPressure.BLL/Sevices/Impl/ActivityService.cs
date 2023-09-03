using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Schedule;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ActivityService (IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task CreateActivity(NewActivity newActivity)
        {
            var activityEntity = _mapper.Map<Activity>(newActivity);
            _uow.ActivityRepository.Create(activityEntity);

            await _uow.SaveAsync();
        }

        public async Task<List<ActivityDTO>> GetAllUserActivity(int userId)
        {
            var userEntity = await _uow.UserRepository.FindAsync(userId);
            
            if (userEntity is null)
            {
                throw new Exception($"There is no user with id {userId}");
            }

            var activitiesEntity = await _uow.ActivityRepository.FindAllUserActivitiesAsync(userId);

            var activities = _mapper.Map<List<ActivityDTO>>(activitiesEntity);

            return activities;
        }

        public async Task<ActivityDTO> UpdateActivity(UpdateActivity updatedActivity)
        {
            var activityEntity = await _uow.ActivityRepository.FindAsync(updatedActivity.Id);

            if(activityEntity is null)
            {
                throw new Exception($"There is no activity with id {updatedActivity.Id}");
            }

            activityEntity.Name = updatedActivity.Name;
            activityEntity.Description = updatedActivity.Description;

            _uow.ActivityRepository.Update(activityEntity);

            await _uow.SaveAsync();

            return _mapper.Map<ActivityDTO>(activityEntity);
        }

        public async Task DeleteActivity(int activityId)
        {
            var activityEntity = await _uow.ActivityRepository.FindAsync(activityId);

            if(activityEntity is null)
            {
                throw new Exception($"There is no activity with id {activityId}");
            }

            _uow.ActivityRepository.Remove(activityId);

            await _uow.SaveAsync();
        }
    }
}
