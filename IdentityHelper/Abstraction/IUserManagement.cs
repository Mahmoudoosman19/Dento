using IdentityHelper.Models;

namespace IdentityHelper.Abstraction
{
    public interface IUserManagement
    {
        Task<UserModel?> GetUserData(Guid id);
        Task<List<UserModel>> GetUsersData(List<Guid> ids);
    }
}
