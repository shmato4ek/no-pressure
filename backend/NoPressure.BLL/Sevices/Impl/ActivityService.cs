using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Tag;
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
            var activityEntity = new Activity() {
                UserId = newActivity.UserId,
                StartTime = ScheduleHour.Undefined,
                EndTime = ScheduleHour.Undefined,
                Date = DateTime.UtcNow,
                Name = newActivity.Name,
                Description = newActivity.Description,
                IsRepeatable = newActivity.IsRepeatable
            };

            if(newActivity.Tag is null)
            {
                newActivity.Tag = "Other";
            }

            var tagEntity = await _uow.TagRepository.FindByNameAsync(newActivity.Tag, newActivity.UserId);

            if(tagEntity is null)
            {
                
                var newTagEntity = new Tag() {
                    Name = newActivity.Tag,
                    UserId = activityEntity.UserId,
                    Color = newActivity.Color,
                    Activities = new List<Activity>(),
                };

                if(newActivity.TeamId != 0)
                {
                    var team = await _uow.TeamRepository.FindAsync((int)newActivity.TeamId);
                    newTagEntity.Team = team;
                }

                _uow.TagRepository.Create(newTagEntity);
                
                await _uow.SaveAsync();

                var createdTag = await _uow.TagRepository.FindByNameAsync(newActivity.Tag, newActivity.UserId);

                activityEntity.TagId = createdTag.Id;
            }

            else
            {
                activityEntity.TagId = tagEntity.Id;
                tagEntity.Color = newActivity.Color;
                _uow.TagRepository.Update(tagEntity);
            }

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
            activityEntity.StartTime = updatedActivity.StartTime;
            activityEntity.EndTime = updatedActivity.EndTime;

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

        public async Task<int> CreateTag(NewTag newTag)
        {
            var newTagEntity = new Tag() {
              Name = newTag.Name,
              UserId = newTag.UserId,
              Color = newTag.Color,
              Activities = new List<Activity>(),
            };

            _uow.TagRepository.Create(newTagEntity);
            
            await _uow.SaveAsync();

            var tagId = _uow.TagRepository.FindByNameAsync(newTag.Name, newTag.UserId).Result.Id;
            return tagId;
        }

        public async Task<ActivityDTO> GetActivityById(int activityId)
        {
            var activity = await _uow.ActivityRepository.FindAsync(activityId);

            return _mapper.Map<ActivityDTO>(activity);
        }

        public async Task RemoveFromSchedule(int activityId)
        {
            var activity = await _uow.ActivityRepository.FindAsync(activityId);

            activity.Date = default;
            activity.StartTime = ScheduleHour.Undefined;
            activity.EndTime = ScheduleHour.Undefined;
            activity.IsScheduled = false;

            _uow.ActivityRepository.Update(activity);
        }

        public async Task ChangeState(UpdateActivityState updateActivity)
        {
            var activity = await _uow.ActivityRepository.FindAsync(updateActivity.Id);

            activity.State = updateActivity.State;

            _uow.ActivityRepository.Update(activity);
            await _uow.SaveAsync();
        }

        public async Task<List<ActivityDTO>> GetActivitiesByDate(DateTime date)
        {
            var activities = await _uow.ActivityRepository.GetActivitiesByDate(date);
            if (activities == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<List<ActivityDTO>>(activities);
            }
        }

        public async Task AddNewGoalActivities(List<NewActivity> activities, int tagId, int planId)
        {
            if(!activities.Any())
            {
                throw new Exception();
            }

            var activitiesEntity = new List<Activity>();

            foreach(var activity in activities)
            {
                var activityEntity = new Activity() {
                    UserId = activity.UserId,
                    StartTime = ScheduleHour.Undefined,
                    EndTime = ScheduleHour.Undefined,
                    Name = activity.Name,
                    Description = activity.Description,
                    IsRepeatable = activity.IsRepeatable,
                    PlanId = planId
                };

                activityEntity.TagId = tagId;
                activitiesEntity.Add(activityEntity);
            }
            await _uow.ActivityRepository.BulkInsert(activitiesEntity);
            await _uow.SaveAsync();
        }

        public async Task<List<ActivityDTO>> GetAllTeamActivities(int teamId)
        {
            var teamEntity = await _uow.TeamRepository.FindAsync(teamId);
            
            if (teamEntity is null)
            {
                throw new Exception($"There is no user with id {teamId}");
            }

            var activitiesEntity = await _uow.ActivityRepository.GetAllTeamActivities(teamId);

            var activities = _mapper.Map<List<ActivityDTO>>(activitiesEntity);

            return activities;
        }
    }
}
