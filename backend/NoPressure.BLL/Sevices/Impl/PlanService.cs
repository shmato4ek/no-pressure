using AutoMapper;
using NoPressure.BLL.Exceptions;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Plan;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IActivityService _activityService;
        public PlanService (IUnitOfWork uow, IMapper mapper, IActivityService activityService)
        {
            _uow = uow;
            _mapper = mapper;
            _activityService = activityService;
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

        public async Task ChangeState(PlanChangeState updatedPlan)
        {
            var plan = await _uow.PlanRepository.FindAsync(updatedPlan.Id);

            plan.State = updatedPlan.State;

            _uow.PlanRepository.Update(plan);

            await _uow.SaveAsync();
        }

        public async Task<AllGoals> GetAllGoals(int userId)
        {
            var userEntity = await _uow.UserRepository.FindAsync(userId);

            if(userEntity is null)
            {
                throw new NotFoundException("User", userId);
            }

            var activeGoals = _uow.PlanRepository.GetAllActiveGoals(userId).Result;
            var closedGoals = _uow.PlanRepository.GetAllClosedGoals(userId).Result;
            
            var goals = new AllGoals() {
                ActiveGoals = new List<GoalInfoDTO>(),
                ClosedGoals = new List<GoalInfoDTO>(),
            };

            foreach(var plan in activeGoals)
            {
                var activitiesEntity = plan.Activities;
                double doneActivities = activitiesEntity.Where(a => a.State == ActivityState.Done).Count();
                double allActivities = activitiesEntity.Count;
                int progress = 0;

                if(allActivities != 0)
                {
                    progress = (int)Math.Round(doneActivities / allActivities * 100);
                }

                var activities = _mapper.Map<List<ActivityDTO>>(plan.Activities);

                var foundGoal = new GoalInfoDTO()
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    ActiveActivities = activities.Where(a => a.State == ActivityState.Active).ToList(),
                    DoneActivities = activities.Where(a => a.State == ActivityState.Done).ToList(),
                    DoneTasksAmmount = (int)doneActivities,
                    AllTasksAmmount = (int)allActivities,
                    Progress = progress,
                    GoalState = plan.GoalState
                };

                goals.ActiveGoals.Add(foundGoal);
            }

            foreach(var plan in closedGoals)
            {
                var activitiesEntity = plan.Activities;
                double doneActivities = activitiesEntity.Where(a => a.State == ActivityState.Done).Count();
                double allActivities = activitiesEntity.Count;
                int progress = 0;

                if(allActivities != 0)
                {
                    progress = (int)Math.Round(doneActivities / allActivities * 100);
                }

                var activities = _mapper.Map<List<ActivityDTO>>(plan.Activities);

                var foundGoal = new GoalInfoDTO()
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    ActiveActivities = activities.Where(a => a.State == ActivityState.Active).ToList(),
                    DoneActivities = activities.Where(a => a.State == ActivityState.Done).ToList(),
                    DoneTasksAmmount = (int)doneActivities,
                    AllTasksAmmount = (int)allActivities,
                    Progress = progress,
                    GoalState = plan.GoalState,
                };

                goals.ClosedGoals.Add(foundGoal);
            }            

            return goals;
        }

        public async Task<List<PlanDTO>> GetAllNoGoalPlans(int userId)
        {
            var userEntity = await _uow.UserRepository.FindAsync(userId);

            if(userEntity is null)
            {
                throw new NotFoundException("User", userId);
            }

            var plansEntity = _uow.PlanRepository.GetAllNoGoalPlans(userId).Result;
            
            return _mapper.Map<List<PlanDTO>>(plansEntity);
        }

        public async Task DeletePlan(int planId)
        {
            var planEntity = await _uow.ActivityRepository.FindAsync(planId);

            if(planEntity is null)
            {
                throw new NotFoundException("Plan", planId);
            }

            _uow.PlanRepository.Remove(planId);

            await _uow.SaveAsync();
        }

        public async Task UpdatePlan(UpdatePlan updatedPlan)
        {
            var planEntity = await _uow.PlanRepository.FindAsync(updatedPlan.Id);

            if(planEntity is null)
            {
                throw new NotFoundException("Plan", updatedPlan.Id);
            }

            planEntity.Name = updatedPlan.Name;

            _uow.PlanRepository.Update(planEntity);

            await _uow.SaveAsync();
        }

        public async Task ConvertToGoal(GoalDTO goal)
        {
            var planEntity = await _uow.PlanRepository.FindAsync(goal.Id);

            if(planEntity == null)
            {
                throw new NotFoundException("Plan", goal.Id);
            }

            planEntity.State = PlanState.Goal;
            planEntity.Name = goal.Name;
            
            var tagId = await _activityService.CreateTag(goal.Tag, goal.Id);

            if (goal.Activities.Any())
            {
                await _activityService.AddNewGoalActivities(goal.Activities, tagId, goal.Id);
            }
        }

        public async Task ChangeGoalState(GoalChangeState goal)
        {
            var goalEntity = await _uow.PlanRepository.FindAsync(goal.Id);

            if (goalEntity is null)
            {
                throw new NotFoundException("Goal", goal.Id);
            }

            goalEntity.GoalState = goal.State;

            _uow.PlanRepository.Update(goalEntity);

            await _uow.SaveAsync();
        }
    }
}
