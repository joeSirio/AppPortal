using AppPortal.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AppPortal.Data.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UserService(DataContext dataContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User Get(int id)
        {
            return _dataContext.Users.Find(id);
        }

        public List<User> GetAllUsers()
        {
            return _dataContext.Users.ToList();
        }

        public async Task<List<string>> GetRolesForUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var  roles = await _userManager.GetRolesAsync(user) as List<string>;
            return roles;
        }

        public List<Role> getAllRoles()
        {
            return _dataContext.Roles.ToList();
        }

        public async Task AddRole(Role role)
        {
            var roleCreated = await _roleManager.CreateAsync(role);
        }

        public async Task RemoveRole(int id)
        {
            var role = _dataContext.Roles.Find(id);
            await _roleManager.DeleteAsync(role);
        }

        public void AddUser(User user)
        {
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var dbo = _dataContext.Users.Find(user.Id);
            dbo.Email = user.Email;
            _dataContext.SaveChanges();
        }

        public async Task DeleteUser(int id)
        {
            await _userManager.DeleteAsync(_dataContext.Users.Find(id));
        }

        public async Task updateUserRole(int id, List<string> roles)
        {
            var user = _dataContext.Users.Find(id);
            var addUserRoles = roles.Where(w => !user.Roles.Any(a => a.Name == w)).ToList();
            var removeUserRoles = user.Roles?.Where(w => roles.Any(a => a == w.Name)).Select(s => s.Name).ToList();

            foreach (var role in addUserRoles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            foreach(var role in removeUserRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
        }

        public Role GetRole(int id)
        {
            return _roleManager.Roles.SingleOrDefault(s => s.Id == id);
        }
    }
}
