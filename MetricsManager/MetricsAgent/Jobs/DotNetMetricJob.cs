using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob
    {
        private readonly IServiceProvider _provider;

        private IDotNetMetricsRepository _repository;

        private PerformanceCounter _DotNetCounter;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IDotNetMetricsRepository>();
            _DotNetCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"); 
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости DotNet
            var DotNetUsageInPercents = Convert.ToInt32(_DotNetCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new DotNetMetric { Time = time, Value = DotNetUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
