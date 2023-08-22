using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Plan;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public PlanService (IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddNewPlan(NewPlan newPlan)
        {
            var planEntity = new Plan()
            {
                UserId = newPlan.UserId,
                Name = newPlan.Name
            };
            
            _uow.PlanRepository.Create(planEntity);

            await _uow.SaveAsync();
        }

        public async Task<List<PlanDTO>> GetAllNoGoalPlans(int userId)
        {
            var userEntity = await _uow.UserRepository.FindAsync(userId);

            if(userEntity is null)
            {
                throw new Exception($"There is no user with id {userId}");
            }

            var plansEntity = _uow.PlanRepository.GetAllNoGoalPlans(userId).Result;
            
            return _mapper.Map<List<PlanDTO>>(plansEntity);
        }
    }
}
