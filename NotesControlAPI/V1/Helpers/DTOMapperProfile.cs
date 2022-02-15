using AutoMapper;
using NotesControlAPI.V1.DTOS;
using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Helpers
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<UserModel, UserDTO>();
            CreateMap<UserModel, UserDTO>().ReverseMap();
            CreateMap<CustomerModel, CustomerDTO>();
            CreateMap<CustomerModel, CustomerDTO>().ReverseMap();
            CreateMap<RevenueModel, RevenueDTO>();
            CreateMap<RevenueModel, RevenueDTO>().ReverseMap();
            CreateMap<CategoryModel, CategoryDTO>();
            CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
            CreateMap<ExpenseModel, ExpenseDTO>();
            CreateMap<ExpenseModel, ExpenseDTO>().ReverseMap();
            CreateMap<ConfigurationModel, ConfigurationDTO>();
            CreateMap<ConfigurationModel, ConfigurationDTO>().ReverseMap();
        }
        
    }
}
