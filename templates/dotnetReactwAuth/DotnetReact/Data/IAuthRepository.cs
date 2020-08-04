using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetReact.Models;

namespace DotnetReact.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
         Task<User> GetUser(string username);
         Task<IEnumerable<User>> GetUsers();
         Task<bool> SaveAll();
         Task<User> DeleteUser(string username);
    }
}