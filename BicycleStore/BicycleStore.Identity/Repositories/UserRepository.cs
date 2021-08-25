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

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            SignInManager = signInManager;
        }

        public IQueryable<User> Users { get => userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role); }
        public SignInManager<User> SignInManager { get; }

        public Task<User> GetUserAsync(ClaimsPrincipal id)
        {
            return userManager.GetUserAsync(id);
        }

        public Task<User> GetUserAsync(string email)
        {
            return userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> ChangePasswordAsync(User user, string oldPassword, string newPassord)
        {
            return (await userManager.ChangePasswordAsync(user, oldPassword, newPassord)).Succeeded;
        }

        public async Task<bool> ChangeEmailAsync(User user, string email, string token)
        {
            return (await userManager.ChangeEmailAsync(user, email, token)).Succeeded;
        }

        public async Task<bool> ChangePhoneAsync(User user, string phone, string token)
        {
            return (await userManager.ChangePhoneNumberAsync(user, phone, token)).Succeeded;
        }

        public Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return userManager.CreateAsync(user, password);
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
