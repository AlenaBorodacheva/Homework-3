using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.Models;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Профили для мапинга Cpu метрик
            CreateMap<CpuMetricDto, CpuMetric>().ForMember(dbModel => dbModel.Time,
                                          o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<CpuMetric, CpuMetricDto>().ForMember(tm => tm.Time,
                                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            // Профили для мапинга DotNet метрик
            CreateMap<DotNetMetricDto, DotNetMetric>().ForMember(dbModel => dbModel.Time,
                                          o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<DotNetMetric, DotNetMetricDto>().ForMember(tm => tm.Time,
                                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            // Профили для мапинга Hdd метрик
            CreateMap<HddMetricDto, HddMetric>().ForMember(dbModel => dbModel.Time,
                                         o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<HddMetric, HddMetricDto>().ForMember(tm => tm.Time,
                                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            // Профили для мапинга Network метрик
            CreateMap<NetworkMetricDto, NetworkMetric>().ForMember(dbModel => dbModel.Time,
                                         o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<NetworkMetric, NetworkMetricDto>().ForMember(tm => tm.Time,
                                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            // Профили для мапинга Ram метрик
            CreateMap<RamMetricDto, RamMetric>().ForMember(dbModel => dbModel.Time,
                                         o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<RamMetric, RamMetricDto>().ForMember(tm => tm.Time,
                                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
        }
    }
}
