using Application.RepositoryInterface;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        public async Task<List<Race>>  GetAllUserRaces()
        {
            var curUser = _httpContext.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(x => x.AppUserId == curUser);
            return userRaces.ToList();
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContext.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(x => x.AppUserId == curUser);
            return userClubs.ToList();
        }
        //AS;LDKJFKASDJFKLDSAJFLKJASDFASDJFALK;DSDJF;KASDJFLKDSJ
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            var result = _context.SaveChanges();
            if (result < 0)
                return false;
            return true;
        }

        
       
    }
}
