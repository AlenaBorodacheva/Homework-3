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
            CreateMap<CpuMetricDto, CpuMetricDto>();
            CreateMap<DotNetMetricDto, DotNetMetricDto>();
            CreateMap<HddMetricDto, HddMetricDto>();
            CreateMap<NetworkMetricDto, NetworkMetricDto>();
            CreateMap<RamMetricDto, RamMetricDto>();
        }
    }
}
