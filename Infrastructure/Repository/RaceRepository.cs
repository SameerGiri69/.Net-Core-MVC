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
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Race race)
        {
            _context.Races.Add(race);
            return Save();

        }

        public bool Delete(Race race)
        {
            _context.Races.Remove(race);
            return Save();
        }

        public Task<List<Race>> GetClubByCity(string city)
        {
            return _context.Races.Where(x => x.Address.City.Contains(city)).ToListAsync();

        }

        public List<Race> GetRace()
        {
            var race = _context.Races.ToList();
            return race;
        }
        public Race GetRaceById(int id)
        {
            var races = _context.Races
                .Include("Address")
                .FirstOrDefault(x => x.Id == id);

            return races;

        }
        public Race GetRaceByIdNoTracking(int id)
        {
            var races = _context.Races
                .Include("Address").AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            return races;

        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            if (result < 0)
                return false;
            return true;

        }

        public bool Update(Race data)
        {

            var races = _context.Races.FirstOrDefault(x => x.Id == data.Id);
            races.Title = data.Title;
            races.Description = data.Description;
            races.Image = data.Image;
            races.Id = data.Id;
            races.RaceCategory = data.RaceCategory;
            races.Address = data.Address;
            races.AddressId = data.AddressId;
            var result = Save();
            return result;
        }
        public bool Delete(int id)
        {
            var races = _context.Races.Where(x => x.Id == id).FirstOrDefault();
            _context.Remove(races);
            return Save();
        }
    }
}
