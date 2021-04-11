using System;
using System.Threading.Tasks;
using Quartz;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly INetworkMetricsRepository _repository;

        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<INetworkMetricsRepository>();
            _networkCounter = new PerformanceCounter("Memory", "% Time in GC"); // очень не уверена, что правильно
        }

        public Task Execute(IJobExecutionContext context)
        {
            var networkUsageInPercents = Convert.ToInt32(_networkCounter.NextValue());

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new NetworkMetricDto { Time = time, Value = networkUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
