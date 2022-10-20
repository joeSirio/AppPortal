using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortal.Data.Interfaces
{
    public interface IUserService
    {
        User Get(int id);
        List<User> GetAllUsers();
        Task<List<string>> GetRolesForUser(int id);
        List<Role> getAllRoles();
        Task AddRole(Role role);
        Task RemoveRole(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        Task DeleteUser(int id);
        Task updateUserRole(int id, List<string> roles);
        Role GetRole(int id);
    }
}
