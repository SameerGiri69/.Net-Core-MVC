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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool Add(AppUser user)
        {
            return _userRepository.Add(user);
        }

        public bool Delete(AppUser user)
        {
            return _userRepository.Delete(user);
        }

        public Task<List<AppUser>> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public Task<AppUser> GetById(string id)
        {
            return _userRepository.GetById(id);
        }

        public bool Save()
        {
            return _userRepository.Save();
        }

        public bool Update(AppUser user)
        {
            return _userRepository.Update(user);    
        }
    }
}
