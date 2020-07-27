using AspNetCoreODataWithModel.Data.Entities;
using AspNetCoreODataWithModel.Model;
using AutoMapper;

namespace AspNetCoreODataWithModel.Data.Mapper
{
    /// <summary>
    /// MappingProfile
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MappingProfile()
        {
            CreateMap<TaskModel, Tarea>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                                        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                                        .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.Observations))
                                        .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Date))
                                        .ForMember(dest => dest.Facturable, opt => opt.MapFrom(src => src.Billable))
                                        .ReverseMap();
        }
    }
}
