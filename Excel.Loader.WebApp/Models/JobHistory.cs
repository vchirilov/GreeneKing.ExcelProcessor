﻿using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
using OfficeOpenXml.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class JobHistory: IModel
    {
        [Order(1)]
        public string JobName { get; set; }
        [Order(2)]
        public string StepName { get; set; }
        [Order(3)]
        [EpplusTableColumn(NumberFormat = "MM/dd/yyyy HH:mm:ss")]
        public DateTime LastRunDateTime { get; set; } = default;
        [Order(4)]
        public TimeOnly LastRunDuration { get; set; }  = default;
        [Order(5)]
        public string PackageName { get; set; }

        public bool IsEmpty()
        {
            if (JobName.IsNullOrEmpty() == true
                    && StepName.IsNullOrEmpty() == true
                    && LastRunDateTime == default
                    && LastRunDuration == default
                    && PackageName.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator JobsHistory(JobHistory model)
        {
            return new JobsHistory
            {
                JobName = model.JobName,
                StepName = model.StepName,
                LastRunDateTime = model.LastRunDateTime,
                LastRunDuration = model.LastRunDuration.ToTimeSpan(),
                PackageName = model.PackageName
            };
        }

        public static explicit operator JobHistory(JobsHistory dal)
        {
            return new JobHistory
            {
                JobName = dal.JobName,
                StepName = dal.StepName,
                LastRunDateTime = dal.LastRunDateTime ?? default,
                LastRunDuration = TimeOnly.FromDateTime(new DateTime(dal.LastRunDuration.Value.Ticks)),
                PackageName = dal.PackageName
            };
        }
    }
}