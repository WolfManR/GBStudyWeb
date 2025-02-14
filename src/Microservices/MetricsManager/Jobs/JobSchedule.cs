﻿using System;

namespace MetricsManager.Jobs
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }

        public Type JobType { get; init; }
        public string CronExpression { get; init; }
    };
}