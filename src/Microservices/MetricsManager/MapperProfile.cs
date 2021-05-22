using AutoMapper;
using Common;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DataBase.Models;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            var toTimeConverter = new DateTimeOffsetFormatter();

            CreateMap<AgentInfo, GetAgentResponse>();
            CreateMap<RegisterAgentRequest, AgentInfo>();
            
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