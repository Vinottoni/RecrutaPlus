using AutoMapper;
using RecrutaPlus.Application.Filters;
using RecrutaPlus.Application.ViewModels;
using RecrutaPlus.Domain.Entities;

namespace RecrutaPlus.Application.AutoMapper
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile() 
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Login, LoginViewModel>().ReverseMap();
            CreateMap<Office, OfficeViewModel>().ReverseMap();

            //ParamFilterViewModel
            CreateMap<EmployeeFilter, EmployeeFilterViewModel>().ReverseMap();
            CreateMap<LoginFilter, LoginFilterViewModel>().ReverseMap();
            CreateMap<OfficeFilter, OfficeFilterViewModel>().ReverseMap();
        }
    }
}
