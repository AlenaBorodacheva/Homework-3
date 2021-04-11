using System;
using System.Threading.Tasks;
using Quartz;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IRamMetricsRepository _repository;

        private PerformanceCounter _ramCounter;

        public RamMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IRamMetricsRepository>();
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new RamMetricDto { Time = time, Value = ramUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
