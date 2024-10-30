using AutoMapper;
using SistemaBoleto.Models;

namespace SistemaBoleto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteDto, Cliente>();
            CreateMap<Cliente, ClienteDto>();
        }
    }
}
