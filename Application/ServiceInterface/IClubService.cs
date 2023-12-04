using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterface
{
    public interface IClubService
    {
        public List<Club> GetClubs();
        public Club GetClubById(int id);
        public Club GetClubByIdNoTracking(int id);
        Task<List<Club>> GetClubByCity(string city);
        bool Add(Club club);
        bool Update(Club club);
        bool Delete(int id);
        bool Save();
    }
}
