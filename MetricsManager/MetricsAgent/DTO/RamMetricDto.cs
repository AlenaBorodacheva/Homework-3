using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class RamMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }

    public class RamMetricCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }

    public class AllRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}