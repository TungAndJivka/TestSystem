using TestSystem.DTO;

namespace TestSystem.Services.Contracts
{
    public interface IUserService
    {
        UserDto GetUserByIdWithTests(string userId);
    }
}
