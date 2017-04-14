using Sefacan.Core.Entities;
using Sefacan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sefacan.Service
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IRepository<User> userRepository;
        #endregion

        #region Ctor
        public UserService(IRepository<User> _userRepository)
        {
            userRepository = _userRepository;
        }
        #endregion

        #region Methods
        public User GetById(int Id)
        {
            return userRepository.Find(x => x.Id == Id);
        }

        public IEnumerable<User> GetUsers()
        {
            return userRepository.TableNoTracking.ToList();
        }

        public User CheckUser(string userName, string password)
        {
            return userRepository.Find(x => x.UserName == userName && x.Password == password);
        }
        
        public bool InsertUser(User user)
        {
            return userRepository.Insert(user);
        }

        public bool UpdateUser(User user)
        {
            return userRepository.Delete(user);
        }

        public bool DeleteUser(User user)
        {
            return userRepository.Delete(user);
        }
        #endregion
    }
}