using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IActivityService _activityService;
        public ScheduleService (IUnitOfWork uow, IMapper mapper, IActivityService activityService)
        {
            _uow = uow;
            _mapper = mapper;
            _activityService = activityService;
        }

        public async Task AddActivityToSchedule(AddTaskToSchedule activity)
        {
            var activityEntity = await _uow.ActivityRepository.FindAsync(activity.ActivityId);

            if(activityEntity is null)
            {
                throw new Exception($"Activity with id {activity.ActivityId} was not found");
            }

            activityEntity.Date = DateTime.UtcNow;
            activityEntity.StartTime = (ScheduleHour)activity.StartTime;
            activityEntity.EndTime = (ScheduleHour)activity.EndTime;

            _uow.ActivityRepository.Update(activityEntity);

            await _uow.SaveAsync();
        }

        public async Task<Schedule> GetScheduleAndActivities(int userId)
        {
            var activities = await _activityService.GetAllUserActivity(userId);

            var now = DateTime.Now;
            
            var todayActivities = activities.Where(activity => activity.Date.Date == now.Date).ToList();

            var hours = await CreateSchedule(todayActivities);

            var schedule = new Schedule() {
                Activities = activities,
                Hours = hours
            };

            return schedule;
        }

        private async Task<List<ScheduleTime>> CreateSchedule(List<ActivityDTO> activities)
        {
            var schedule = new List<ScheduleTime>();

            for(int hour = 6; hour <= 23; hour++)
            {
                var scheduleHour = new ScheduleTime()
                {
                    Hour = (ScheduleHour)hour,
                };

                schedule.Add(scheduleHour);
            }

            foreach(var activity in activities)
            {
                for(int hour = (int)activity.StartTime; hour < (int)activity.EndTime; hour++)
                {
                    schedule.Where(h => (int)h.Hour == hour).FirstOrDefault().Activity = activity;
                }
            }

            foreach (var time in schedule)
            {
                if (time.Hour != ScheduleHour.Six)
                {
                    var previous = schedule.Where(h => (int)h.Hour == ((int)time.Hour - 1)).FirstOrDefault().Activity;
                    if(previous != null)
                    {
                        time.HasPrevious = true;
                    }
                }

                if(time.Hour != ScheduleHour.TwentyThree)
                {
                    var next = schedule.Where(h => (int)h.Hour == ((int)time.Hour + 1)).FirstOrDefault().Activity;
                    if(next != null)
                    {
                        time.HasNext = true;
                    }
                }
            }

            return schedule;
        }

    }
}
