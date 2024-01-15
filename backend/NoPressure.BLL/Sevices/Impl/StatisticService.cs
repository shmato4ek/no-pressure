using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Statistic;
using NoPressure.Common.Models.Tag;
using NoPressure.Common.Statistic;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        private readonly IActivityService _activityService;
        private readonly IScheduleService _scheduleService;
        public StatisticService (IUnitOfWork uow,
                                IMapper mapper,
                                ITagService tagService,
                                IActivityService activityService,
                                IScheduleService scheduleService)
        {
            _uow = uow;
            _mapper = mapper;
            _tagService = tagService;
            _scheduleService = scheduleService;
            _activityService = activityService;
        }

        public async Task<ActivitiesStatistic> GetActivitiesStatistic(int userId)
        {
            var statistic = new ActivitiesStatistic();

            var followers = await _uow.SubscriptionRepository.GetAllUsersFollowers(userId);
            var followings = await _uow.SubscriptionRepository.GetAllUsersFollowings(userId);

            statistic.Followers = followers.Any() ? followers.Count : 0;
            statistic.Followings = followings.Any() ? followings.Count : 0;
            
            var userActivities = await _uow.ActivityRepository.FindAllUserActivitiesAsync(userId);
            if(userActivities != null)
            {
                var doneActivities = userActivities
                    .Where(activity => activity.State == ActivityState.Done)
                    .ToList();
                
                if(doneActivities != null)
                {
                    if(userActivities.Count != 0)
                    {
                        statistic.QualityAllTime = Math.Round((double)doneActivities.Count / (double)userActivities.Count * 100);
                    }
                }

                else
                {
                    statistic.QualityAllTime = 0;
                }
            }
            
            var weekAgoDate = DateTime.UtcNow.AddDays(-7);
            var weekActivities = userActivities
                .Where(activity => activity.Date > weekAgoDate)
                .ToList();

            if (weekActivities != null)
            {
                var weekDoneActivities = weekActivities
                    .Where(activity => activity.State == ActivityState.Done)
                    .ToList();
                
                if(weekDoneActivities != null)
                {
                    if(weekActivities.Count != 0)
                    {
                        statistic.QualityWeek = Math.Round((double)weekDoneActivities.Count / (double)weekActivities.Count * 100);
                    }
                }
                else
                {
                    statistic.QualityWeek = 0;
                }
            }

            var monthAgoDate = DateTime.UtcNow.AddMonths(-1);
            var monthActivities = userActivities
                .Where(activity => activity.Date > monthAgoDate)
                .ToList();

            if(monthActivities != null)
            {
                var monthDoneActivities = monthActivities
                    .Where(activity => activity.State == ActivityState.Done)
                    .ToList();

                if(monthDoneActivities != null)
                {
                    if(monthActivities.Count != 0)
                    {
                        statistic.QualityMonth = Math.Round((double)monthDoneActivities.Count / (double)monthActivities.Count * 100);
                    }
                }
                else
                {
                    statistic.QualityMonth = 0;
                }
            }

            var tagsStatistic = new List<TagStatistic>();

            var userTags = await _tagService.GetBestUserTags(userId);

            if (userTags != null)
            {
                foreach(var tag in userTags)
                {
                    var tagStatistic = new TagStatistic();
                    tagStatistic.Name = tag.Name;
                    var doneTagTasks = tag.Activities
                        .Where(activity => activity.State == ActivityState.Done)
                        .ToList();

                    if(doneTagTasks != null)
                    {
                        if (tag.Activities.Count != 0)
                        {
                            tagStatistic.Quality = Math.Round((double)doneTagTasks.Count / (double)tag.Activities.Count * 100);
                        }
                    }
                    else
                    {
                        tagStatistic.Quality = 0;
                    }

                    tagsStatistic.Add(tagStatistic);
                }
            }

            statistic.TagStatistics = tagsStatistic;

            var scheduleStatistics = new List<ScheduleStatistic>();

            for(int day = 7; day > 0; day--)
            {
                var dayStatistic = new ScheduleStatistic();
                var date = DateTime.UtcNow.AddDays(-day);

                var activities = await _activityService.GetActivitiesByDate(date);
                
                dayStatistic.DayOfWeek = date.DayOfWeek.ToString();


                if(activities != null)
                {
                    dayStatistic.HoursAmmount = _scheduleService.GetHoursOfDoneTasks(activities);
                }
                else
                {
                    dayStatistic.HoursAmmount = 0;
                }
                scheduleStatistics.Add(dayStatistic);
            }

            statistic.Schedule = scheduleStatistics;

            return statistic;
        }
    }
}
