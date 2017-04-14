using Sefacan.Core.Entities;
using System.Collections.Generic;

namespace Sefacan.Service
{
    public interface IUserService
    {
        User GetById(int Id);
        IEnumerable<User> GetUsers();
        User CheckUser(string userName, string password);
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
    }
}
