using System;
using System.Threading.Tasks;
using Quartz;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IHddMetricsRepository _repository;

        private PerformanceCounter _hddCounter;

        public HddMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IHddMetricsRepository>();
            _hddCounter = new PerformanceCounter("Memory", "% Committed Bytes");  // очень не уверена, что правильно
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new HddMetricDto { Time = time, Value = hddUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
