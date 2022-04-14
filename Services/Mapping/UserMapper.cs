using IMDbMovies.Domain;
using Services.Models;

namespace Services.Mapping
{
    public static class UserMapper
    {
        public static User MapToDomain(this UserDto user)
        {
            return new User
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
        }

        public static UserDto MapToDto(this User user)
        {
            return new UserDto()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
        }
    }
}
