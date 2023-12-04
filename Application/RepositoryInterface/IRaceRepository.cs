using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RepositoryInterface
{
    public interface IRaceRepository
    {
        public List<Race> GetRace();
        public Race GetRaceById(int id);
        public Race GetRaceByIdNoTracking(int id);
        Task<List<Race>> GetClubByCity(string city);
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(int id);
        bool Save();
    }
}
