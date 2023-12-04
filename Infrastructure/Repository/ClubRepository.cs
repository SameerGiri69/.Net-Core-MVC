using Application.RepositoryInterface;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Clubs.Add(club);
            return Save();


        }

        public bool Delete(int id)
        {
            var club = _context.Clubs.Where(x => x.Id == id).FirstOrDefault();
            _context.Remove(club);
            return Save();
        }

        public async Task<List<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(x => x.Address.City.Contains(city)).ToListAsync();

        }

        public Club GetClubById(int id)
        {
            var club = _context.Clubs
                .Include("Address")
                .Where(c => c.Id == id).FirstOrDefault();
            return club;
        }
        public Club GetClubByIdNoTracking(int id)
        {
            var club = _context.Clubs.Include("Address").AsNoTracking().FirstOrDefault(c => c.Id == id);
            return club;
        }

        public List<Club> GetClubs()
        {
            var clubs = _context.Clubs.ToList();
            return clubs;
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            if (result < 0)
                return false;
            return true;

        }

        public bool Update(Club data)
        {

            var clubs = _context.Clubs.FirstOrDefault(x => x.Id == data.Id);
            clubs.Title = data.Title;
            clubs.Description = data.Description;
            clubs.Image = data.Image;
            clubs.Id = data.Id;
            clubs.ClubCategory = data.ClubCategory;
            clubs.Address = data.Address;
            clubs.AddressId = data.AddressId;
            var result = Save();
            return result;
        }
    }
}
