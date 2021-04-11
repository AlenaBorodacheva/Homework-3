using System;
using System.Threading.Tasks;
using Quartz;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IDotNetMetricsRepository _repository;

        private PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IDotNetMetricsRepository>();
            _dotnetCounter = new PerformanceCounter("Memory", "Private Bytes"); // очень не уверена, что правильно
        }

        public Task Execute(IJobExecutionContext context)
        {
            var dotnetUsageInPercents = Convert.ToInt32(_dotnetCounter.NextValue());

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new DotNetMetricDto { Time = time, Value = dotnetUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
