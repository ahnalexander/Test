using AutoMapper;
using MyApp.Application.Companies.Models;
using MyApp.Application.Employees.Models;
using MyApp.Domain;

namespace MyApp.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<UpdateEmployeeModel, Employee>();

        CreateMap<Company, CompanyDto>();
        CreateMap<UpdateCompanyModel, Company>();
    }
}