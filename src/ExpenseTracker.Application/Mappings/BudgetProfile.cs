using AutoMapper;
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.A.Entities.Mappings;

public class BudgetProfile : Profile
{
    public BudgetProfile()
    {
        CreateMap<BudgetCreateDto, Budget>();
        
        CreateMap<Budget, BudgetDto>().ReverseMap();
    }
}