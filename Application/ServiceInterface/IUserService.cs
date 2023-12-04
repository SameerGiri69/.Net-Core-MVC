using Domain.Models;

namespace RunningGroups.Interface
{
    public interface IUserService
    {
        Task<List<AppUser>> GetAllUsers();
        Task<AppUser> GetById(string id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();

    }
}
