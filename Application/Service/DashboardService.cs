using Application.RepositoryInterface;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using RunningGroups.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public Task<List<Club>> GetAllUserClubs()
        {
            return _dashboardRepository.GetAllUserClubs();
        }

        public Task<List<Race>> GetAllUserRaces()
        {
            return _dashboardRepository.GetAllUserRaces();
        }

        public Task<AppUser> GetUserById(string id)
        {
            return _dashboardRepository.GetUserById(id);
        }

        public Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return _dashboardRepository.GetUserByIdNoTracking(id);
        }

        public bool Update(AppUser user)
        {
            return _dashboardRepository.Update(user);
        }
    }
}
