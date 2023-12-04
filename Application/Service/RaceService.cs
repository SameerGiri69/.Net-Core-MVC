using Application.RepositoryInterface;
using Application.ServiceInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class RaceService : IRaceService
    {
        private readonly IRaceRepository _raceRepository;

        public RaceService(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public bool Add(Race race)
        {
            return _raceRepository.Add(race);
        }

        public bool Delete(int id)
        {
            return _raceRepository.Delete(id);
        }

        public async Task<List<Race>> GetClubByCity(string city)
        {
            return await _raceRepository.GetClubByCity(city);
        }

        public List<Race> GetRace()
        {
            return _raceRepository.GetRace();
        }

        public Race GetRaceById(int id)
        {
            return _raceRepository.GetRaceById(id);
        }

        public Race GetRaceByIdNoTracking(int id)
        {
            return _raceRepository.GetRaceByIdNoTracking(id);
        }

        public bool Save()
        {
            return _raceRepository.Save();
        }

        public bool Update(Race race)
        {
            return _raceRepository.Update(race);
        }

    }
}
