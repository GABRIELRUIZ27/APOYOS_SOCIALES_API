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

            CreateMap<FondoDTO, Fondo>();
            CreateMap<Fondo, FondoDTO>()
                .ForMember(dest => dest.TipoDistribucion, opt => opt.MapFrom(src => src.TipoDistribucion));

            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();

            CreateMap<TipoDistribucion, TipoDistribucionDTO>();
            CreateMap<TipoDistribucionDTO, TipoDistribucion>();

            CreateMap<Comunidad, ComunidadDTO>();
            CreateMap<ComunidadDTO, Comunidad>();

            CreateMap<Cargo, CargoDTO>();
            CreateMap<CargoDTO, Cargo>();

            CreateMap<Area, AreaDTO>();
            CreateMap<AreaDTO, Area>();

            CreateMap<Genero, GeneroDTO>();
            CreateMap<GeneroDTO, Genero>();

            CreateMap<ProgramaSocial, ProgramaSocialDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area));
            CreateMap<ProgramaSocialDTO, ProgramaSocial>();

            CreateMap<AdquisicionDTO, Adquisicion>();
            CreateMap<Adquisicion, AdquisicionDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area));

            CreateMap<ApoyoDTO, Apoyo>();
            CreateMap<Apoyo, ApoyoDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Comunidad, opt => opt.MapFrom(src => src.Comunidad));

            CreateMap<SolicitudDTO, Solicitud>();
            CreateMap<Solicitud, SolicitudDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Comunidad, opt => opt.MapFrom(src => src.Comunidad));

            CreateMap<PersonalDTO, Personal>();
            CreateMap<Personal, PersonalDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero));

            CreateMap<TipoIncidencia, TipoIncidenciaDTO>();
            CreateMap<TipoIncidenciaDTO, TipoIncidencia>();

            CreateMap<IncidenciaDTO, Incidencia>();
            CreateMap<Incidencia, IncidenciaDTO>()
                .ForMember(dest => dest.TipoIncidencia, opt => opt.MapFrom(src => src.TipoIncidencia))
                .ForMember(dest => dest.Comunidad, opt => opt.MapFrom(src => src.Comunidad));
        }
    }
}