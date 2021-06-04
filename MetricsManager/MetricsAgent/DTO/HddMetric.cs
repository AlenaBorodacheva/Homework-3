using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class HddMetric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }

    public class HddMetricCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }

    public class AllHddMetricsResponse
    {
        public List<HddMetric> Metrics { get; set; }
    }
}
