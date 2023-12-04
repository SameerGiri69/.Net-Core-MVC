using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace RunningGroups.Interface
{
    public interface IDashboardService
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByIdNoTracking(string id);
        bool Update(AppUser user);
        
    }
}
