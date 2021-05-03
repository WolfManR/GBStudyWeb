using AutoMapper;
using Common;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DataBase.Models;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            var toTimeConverter = new DateTimeOffsetFormatter();
            
            CreateMap<CpuMetric, CpuMetricResponse>()
                .ForMember(r => r.Time, exp => exp.ConvertUsing(toTimeConverter, m => m.Time));
            CreateMap<DotnetMetric, DotnetMetricResponse>()
                .ForMember(r => r.Time, exp => exp.ConvertUsing(toTimeConverter, m => m.Time));
            CreateMap<HddMetric, HddMetricResponse>()
                .ForMember(r => r.Time, exp => exp.ConvertUsing(toTimeConverter, m => m.Time));
            CreateMap<NetworkMetric, NetworkMetricResponse>()
                .ForMember(r => r.Time, exp => exp.ConvertUsing(toTimeConverter, m => m.Time));
            CreateMap<RamMetric, RamMetricResponse>()
                .ForMember(r => r.Time, exp => exp.ConvertUsing(toTimeConverter, m => m.Time));
        }
    }
}