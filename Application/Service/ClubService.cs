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
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public bool Add(Club club)
        {
            return _clubRepository.Add(club);
        }

        public bool Delete(int id)
        {
           return _clubRepository.Delete(id); 
        }

        public Task<List<Club>> GetClubByCity(string city)
        {
            return _clubRepository.GetClubByCity(city);
        }

        public Club GetClubById(int id)
        {
            return _clubRepository.GetClubById(id);
        }

        public Club GetClubByIdNoTracking(int id)
        {
            return _clubRepository.GetClubByIdNoTracking(id);
        }

        public List<Club> GetClubs()
        {
            return _clubRepository.GetClubs();
        }

        public bool Save()
        {
            return _clubRepository.Save();
        }

        public bool Update(Club club)
        {
            return _clubRepository.Update(club);
        }
    }
}
