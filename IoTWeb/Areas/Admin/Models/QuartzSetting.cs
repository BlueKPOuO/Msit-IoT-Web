using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IoTWeb.Models
{
    public class QuartzSetting
    {
    }
    public class ExecuteTaskServiceCallScheduler
    {
        private static readonly string ScheduleCronExpression = ConfigurationManager.AppSettings["ExecuteTaskScheduleCronExpression"];

        public static async System.Threading.Tasks.Task StartAsync()
        {
            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();

                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }

                var job = JobBuilder.Create<ExecuteTaskServiceCallJob>()
                    .WithIdentity("ExecuteTaskServiceCallJob1", "group1")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("ExecuteTaskServiceCallTrigger1", "group1")
                    .WithCronSchedule(ScheduleCronExpression)
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {

            }
        }
    }
    public class ExecuteTaskServiceCallJob : IJob
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        ManagementFee mf = new ManagementFee();
        public static readonly string SchedulingStatus = ConfigurationManager.AppSettings["ExecuteTaskServiceCallSchedulingStatus"];
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (SchedulingStatus.Equals("ON"))
                {
                    try
                    {
                        var residentList = db.ResidentDataTable.Where(r => r.Living == true).Select(r => r.ResidentID).ToList();
                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;
                        var check = db.ManagementFee.Where(currentmonth => currentmonth.Month == month && currentmonth.Year == year).FirstOrDefault();

                        if (check is null)
                        {
                            foreach (var resident in residentList)
                            {
                                mf.ResidentID = resident;
                                mf.Year = year;
                                mf.Month = month;
                                mf.Fee = 1000;
                                db.ManagementFee.Add(mf);
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });
            return task;
        }
    }
}