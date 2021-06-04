using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class CpuMetric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }

    public class CpuMetricCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }

    public class AllCpuMetricsResponse
    {
        public List<CpuMetric> Metrics { get; set; }
    }
}
