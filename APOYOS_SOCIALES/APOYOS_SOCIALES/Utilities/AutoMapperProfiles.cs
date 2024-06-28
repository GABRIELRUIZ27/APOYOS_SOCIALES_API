using APOYOS_SOCIALES.DTOs;
using APOYOS_SOCIALES.Entities;
using APOYOSSOCIALES.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel;

namespace simpatizantes_api.Utilities
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Claim, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Rol.Id))
                .IncludeMembers(src => src.Rol);

            CreateMap<Rol, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Id));
            
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol));

            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();

            CreateMap<Comunidad, ComunidadDTO>();
            CreateMap<ComunidadDTO, Comunidad>();

            CreateMap<Area, AreaDTO>();
            CreateMap<AreaDTO, Area>();

            CreateMap<ProgramaSocial, ProgramaSocialDTO>();
            CreateMap<ProgramaSocialDTO, ProgramaSocial>();

            CreateMap<ApoyoDTO, Apoyo>();
            CreateMap<Apoyo, ApoyoDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Comunidad, opt => opt.MapFrom(src => src.Comunidad));

            CreateMap<TipoIncidencia, TipoIncidenciaDTO>();
            CreateMap<TipoIncidenciaDTO, TipoIncidencia>();

            CreateMap<IncidenciaDTO, Incidencia>();
            CreateMap<Incidencia, IncidenciaDTO>()
                .ForMember(dest => dest.TipoIncidencia, opt => opt.MapFrom(src => src.TipoIncidencia))
                .ForMember(dest => dest.Comunidad, opt => opt.MapFrom(src => src.Comunidad));
        }
    }
}