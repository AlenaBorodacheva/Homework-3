using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetric>();
            CreateMap<DotNetMetric, DotNetMetric>();
            CreateMap<HddMetric, HddMetric>();
            CreateMap<NetworkMetric, NetworkMetric>();
            CreateMap<RamMetric, RamMetric>();
        }
    }
}
