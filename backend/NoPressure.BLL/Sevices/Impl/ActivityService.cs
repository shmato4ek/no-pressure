using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.Activity;
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
    }
}
