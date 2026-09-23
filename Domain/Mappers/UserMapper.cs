using Mapster;
using Domain.Dtos;
using Domain.Entities;

namespace Domain.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        return user.Adapt<UserDto>();        
    }
}