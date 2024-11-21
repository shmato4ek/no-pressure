using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Schedule;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.Helpers;

public static class Algorithm
{
    //test
    private static int MAX_HOUR = 23;
    private static int MIN_HOUR = 6;
    
    public static GeneratedSchedule GenerateSchedule(List<ActivityDTO> activities, ScheduleGenerationConfigurationDTO config)
    {
        List<GeneratedSchedule> allSchedules = new List<GeneratedSchedule>();
        GeneratedSchedule firstSchedule = CreateDefaultSchedule();
        
        List<ActivityDTO> sortedActivities = activities.OrderBy(a => a.Priority).ToList();
        
        bool flag = true;
        int hour = MIN_HOUR;
        while (flag)
        {
            var activity = sortedActivities[0];
            if (activity.Duration > MAX_HOUR - hour + 1)
                break;

            for (int i = 0; i < activity.Duration; i++)
            {
                firstSchedule.Hours
                    .Where(h => (int)h.Hour == hour)
                    .FirstOrDefault()
                    .Activity = activity;
                hour++;
            }

            sortedActivities.Remove(activity);
            
            if(sortedActivities.Count == 0)
                flag = false;
        }
        
        firstSchedule.Penalty = GetDirectiveTermPenalty(firstSchedule);
        
        allSchedules.Add(firstSchedule);

        for (int i = 0; i < config.IterationsAmount; i++)
        {
            Console.WriteLine($"\nIteration #{i + 1}\n");
            if (config.IsMutationEnabled || allSchedules.Count < 2)
            {
                allSchedules.Add(Mutation(allSchedules));
            }

            if (config.IsCrossowerEnabled)
            {
                var crossowerResult = Crossower(allSchedules);
                
                if (crossowerResult != null)
                {
                    allSchedules.Add(crossowerResult);
                }
            }

            allSchedules = allSchedules.OrderBy(s => s.Penalty).ToList();
        }
        
        return allSchedules[0];
    }

    private static GeneratedSchedule CreateDefaultSchedule()
    {
        var schedule = new GeneratedSchedule
        {
            Hours = new List<ScheduleTime>()
        };

        for(var hour = MIN_HOUR; hour <= MAX_HOUR; hour++)
        {
            var scheduleHour = new ScheduleTime()
            {
                Hour = (ScheduleHour)hour,
            };

            schedule.Hours.Add(scheduleHour);
        }
        
        return schedule;
    }

    private static GeneratedSchedule Mutation(List<GeneratedSchedule> allSchedules)
    {
        var random = new Random();

        var firstActivity = new ActivityDTO();
        var secondActivity = new ActivityDTO();

        var schedule = allSchedules[0];
        var flag = true;

        while (flag)
        {
            int index1 = random.Next(MIN_HOUR, MAX_HOUR);
            int index2 = random.Next(MIN_HOUR, MAX_HOUR);
            
            firstActivity = schedule.Hours.Where(h => (int)h.Hour == index1).FirstOrDefault().Activity;
            secondActivity = schedule.Hours.Where(h => (int)h.Hour == index2).FirstOrDefault().Activity;

            if (firstActivity != null && secondActivity != null && firstActivity.Id != secondActivity.Id)
            {
                flag = false;
            }
        }

        var newSchedule = CreateNewSchedule(firstActivity, secondActivity, schedule);
        
        newSchedule.Penalty = GetDirectiveTermPenalty(newSchedule);
        
        Console.WriteLine($"Mutation added schedule ({newSchedule.Penalty})!");
        
        return newSchedule;
    }

    private static GeneratedSchedule Crossower(List<GeneratedSchedule> allSchedules)
    {
        Console.WriteLine("Crossower started");
        var firstSchedule = allSchedules[0];
        var secondSchedule = allSchedules[1];
        
        var rand = new Random();
        var flag = true;
        
        var firstActivity = new ActivityDTO();
        var secondActivity = new ActivityDTO();

        int count = 0;

        while (flag)
        {
            var index = rand.Next(MIN_HOUR, MAX_HOUR);

            firstActivity = firstSchedule.Hours
                .FirstOrDefault(h => (int)h.Hour == index)?
                .Activity;

            secondActivity = secondSchedule.Hours
                .FirstOrDefault(h => (int)h.Hour == index)?
                .Activity;

            if ((firstActivity != null && 
                secondActivity != null && 
                firstActivity.Id != secondActivity.Id && 
                firstSchedule.Hours.FirstOrDefault(h => h.Activity.Id == firstActivity.Id) != null &&
                firstSchedule.Hours.FirstOrDefault(h => h.Activity.Id == secondActivity.Id) != null) ||
                count == 10)
            {
                flag = false;
            }
            else
            {
                count++;
            }
        }
        
        Console.WriteLine("After while cycle");
        
        if (firstActivity == null || secondActivity == null || count == 10)
            return null;

        var newSchedule = CreateNewSchedule(firstActivity, secondActivity, firstSchedule);
        
        newSchedule.Penalty = GetDirectiveTermPenalty(newSchedule);
        
        Console.WriteLine($"Crossower added schedule ({newSchedule.Penalty})!");
        
        return newSchedule;
    }

    private static GeneratedSchedule CreateNewSchedule(ActivityDTO firstActivity, 
                                                        ActivityDTO secondActivity, 
                                                        GeneratedSchedule schedule)
    {
        var newSchedule = CreateDefaultSchedule();
        var durationDifference = Math.Abs(firstActivity.Duration - secondActivity.Duration);
        
        var firstActivityHour = (int)schedule.Hours
            .FirstOrDefault(h => h.Activity.Id == firstActivity.Id)
            .Hour;

        var secondActivityHour = (int)schedule.Hours
            .FirstOrDefault(h => h.Activity.Id == secondActivity.Id)
            .Hour;
        
        if(firstActivity.Duration < secondActivity.Duration)
        {
            var isFirstActivityEarlier = firstActivityHour < secondActivityHour;
                
            if (isFirstActivityEarlier)
            {
                Console.WriteLine("Case 1");
                for (var i = MIN_HOUR; i < firstActivityHour; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                            .FirstOrDefault(h => (int)h.Hour == i)
                            .Activity;
                }

                for (var i = firstActivityHour; i < firstActivityHour + secondActivity.Duration; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = secondActivity;
                }

                for (var i = firstActivityHour + secondActivity.Duration;
                     i < secondActivityHour + durationDifference;
                     i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                            .FirstOrDefault(h => (int)h.Hour == i - durationDifference)
                            .Activity;
                }

                for (var i = secondActivityHour + durationDifference; i < secondActivityHour + durationDifference + firstActivity.Duration - 1; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = firstActivity;
                }

                for (var i = secondActivityHour + durationDifference + firstActivity.Duration - 1; i <= MAX_HOUR; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                            .FirstOrDefault(h => (int)h.Hour == i)
                            .Activity;
                }
            }
            else
            {
                Console.WriteLine("Case 2");
                for (var i = MIN_HOUR; i < secondActivityHour; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity;
                }

                for (var i = secondActivityHour; i < secondActivityHour + firstActivity.Duration; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = firstActivity;
                }

                for (var i = secondActivityHour + firstActivity.Duration;
                     i < firstActivityHour - durationDifference;
                     i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i + durationDifference)
                        .Activity;
                }

                for (var i = firstActivityHour - durationDifference; i < firstActivityHour - durationDifference + secondActivity.Duration; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = secondActivity;
                }

                for (var i = firstActivityHour - durationDifference + secondActivity.Duration; i <= MAX_HOUR; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity;
                }
            }
        }
        else if (firstActivity.Duration > secondActivity.Duration)
        {
            if (firstActivityHour < secondActivityHour)
            {
                Console.WriteLine("Case 3");
                for (var i = MIN_HOUR; i < firstActivityHour; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity;
                }

                for (var i = firstActivityHour; i < firstActivityHour + secondActivity.Duration; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = secondActivity;
                }

                for (var i = firstActivityHour + secondActivity.Duration;
                     i < secondActivityHour - durationDifference;
                     i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i + durationDifference)
                        .Activity;
                }

                for (var i = secondActivityHour - durationDifference;
                     i < secondActivityHour - durationDifference + firstActivity.Duration;
                     i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = firstActivity;
                }

                for (var i = secondActivityHour - durationDifference + firstActivity.Duration; i <= MAX_HOUR; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity;
                }
            }
            else
            {
                Console.WriteLine("Case 4");
                for (var i = MIN_HOUR; i < secondActivityHour; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity;
                }

                for (var i = secondActivityHour; i < secondActivityHour + firstActivity.Duration; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = firstActivity;
                }

                for (var i = secondActivityHour + firstActivity.Duration;
                     i < firstActivityHour + durationDifference;
                     i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i - durationDifference)
                        .Activity;
                }

                for (var i = firstActivityHour + durationDifference;
                     i < firstActivityHour + durationDifference + secondActivity.Duration;
                     i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = secondActivity;
                }

                for (var i = firstActivityHour + durationDifference + secondActivity.Duration; i <= MAX_HOUR; i++)
                {
                    newSchedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity = schedule.Hours
                        .FirstOrDefault(h => (int)h.Hour == i)
                        .Activity;
                }
            }
        }
        else
        {
            Console.WriteLine("Case 5");
            for (var i = MIN_HOUR; i <= MAX_HOUR; i++)
            {
                newSchedule.Hours
                    .FirstOrDefault(h => (int)h.Hour == i)
                    .Activity = schedule.Hours
                    .FirstOrDefault(h => (int)h.Hour == i)
                    .Activity;
            }

            for (var i = firstActivityHour; i < firstActivityHour + firstActivity.Duration; i++)
            {
                newSchedule.Hours
                    .FirstOrDefault(h => (int)h.Hour == i)
                    .Activity = secondActivity;
            }

            for (var i = secondActivityHour; i < secondActivityHour + secondActivity.Duration; i++)
            {
                newSchedule.Hours
                    .FirstOrDefault(h => (int)h.Hour == i)
                    .Activity = firstActivity;
            }
        }

        return newSchedule;
    }

    private static double GetDirectiveTermPenalty(GeneratedSchedule schedule)
    {
        double maxDirectiveTermPenalty = 0;
        double currentDirectiveTermPenalty = 0;
        
        var currentActivity = new ActivityDTO();

        foreach (var hour in schedule.Hours)
        {
            if (hour.Activity != null)
            {
                if ((hour.Activity.Id != currentActivity.Id && currentActivity.Id != 0) ||
                    hour.Activity.Id == 0 && currentActivity.Id != 0)
                {
                    maxDirectiveTermPenalty += (double)(currentActivity.DirectiveTerm - (MIN_HOUR + currentActivity.Duration));
                    if (currentActivity.DirectiveTerm < hour.Hour)
                    {
                        currentDirectiveTermPenalty += ((double)hour.Hour - (double)currentActivity.DirectiveTerm) * currentActivity.DelayCoefficient;
                    }
                }

                currentActivity = hour.Activity;
            }
        }
        
        if (maxDirectiveTermPenalty != 0)
        {
            return currentDirectiveTermPenalty / maxDirectiveTermPenalty;
        }
        else
        {
            return 0;
        }
    }
}