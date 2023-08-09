using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ScheduleService (IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ScheduleDTO> AddActivityToSchedule(DateTime date, int activityId, int hour)
        {
            var scheduleEntity = await _uow.ScheduleRepository.FindAsync(date);

            if(scheduleEntity is null)
            {
                throw new Exception($"Schedule with date {date} not found");
            }

            var activityEntity = await _uow.ActivityRepository.FindAsync(activityId);

            if(activityEntity is null)
            {
                throw new Exception($"Activity with id {activityId} not found");
            }

            var scheduleHour = scheduleEntity
                .Time
                .Where(time => time.Hour == hour)
                .FirstOrDefault();

            if (scheduleHour.Activity != null)
            {
                throw new Exception("Hour is already has an activity!");
            }

            scheduleHour.Activity = activityEntity;

            _uow.ScheduleRepository.Update(scheduleEntity);
            await _uow.SaveAsync();

            var schedule = _mapper.Map<ScheduleDTO>(scheduleEntity);

            return schedule;
        }

        public async Task<ScheduleDTO> GetSchedule(DateTime date)
        {
            var scheduleEntity = await _uow.ScheduleRepository.FindAsync(date);
            
            if(scheduleEntity is null)
            {
                throw new Exception($"Schedule with date {date} not found");
            }

            var schedule = _mapper.Map<ScheduleDTO>(scheduleEntity);

            return schedule;
        }
    }
}
