using Week14Practice2_Jwt.Models;
using Week14Practice2_Jwt.Type;

namespace Week14Practice2_Jwt.Services
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);
        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user);

    }
}
