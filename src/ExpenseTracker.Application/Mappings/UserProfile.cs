using AutoMapper;
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Domain.Entities;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
            .ReverseMap();

        CreateMap<Budget, BudgetDto>().ReverseMap();
    }
}