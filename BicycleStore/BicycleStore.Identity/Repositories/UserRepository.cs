using BicycleStore.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.Identity.Repositories
{
    public class UserRepository
    {
        private readonly UserManager<User> userManager;

        public UserRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IQueryable<User> Users { get => userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role); }

        public Task<User> GetUserAsync(ClaimsPrincipal id)
        {
            return userManager.GetUserAsync(id);
        }

        public Task<User> GetUserAsync(string email)
        {
            return userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
        }


        public async Task<bool> AddUserAsync(User user, string password)
        {
            return (await userManager.CreateAsync(user, password)).Succeeded;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            return (await userManager.DeleteAsync(user)).Succeeded;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            return (await userManager.UpdateAsync(user)).Succeeded;
        }

        public async Task<bool> AddToRoleAsync(User user, string role)
        {
            return (await userManager.AddToRoleAsync(user, role)).Succeeded;
        }

        public async Task<bool> RemoveFromRoleAsync(User user, string role)
        {
            return (await userManager.RemoveFromRoleAsync(user, role)).Succeeded;
        }
    }
}
