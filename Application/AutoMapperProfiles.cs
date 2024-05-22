using AutoMapper;
using Application.DTO;
using Application.Entities;

namespace Application
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteCreacionDTO, Cliente>();
            CreateMap<Plan, PlanDTO>();
            CreateMap<PlanCreacionDTO, Plan>();
        }
    }
}
