using BicycleStore.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.Identity.Repositories
{
    public class RoleRepository
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<Role> roleManager;

        public RoleRepository(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IQueryable<Role> Roles { get => roleManager.Roles.Include(x => x.UserRoles).ThenInclude(x => x.User); }

       
        public async Task<Role> GetRoleById(string id)
        {
            return roleManager.Roles?.FirstOrDefault(x => x.Id == id);
        }
        public async Task<Role> GetRole(string name)
        {
            return roleManager.Roles?.FirstOrDefault(x => x.Name == name);
        }
        public async Task<bool> CreateRoleAsync(Role role)
        {
            return (await roleManager.CreateAsync(role)).Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(Role role)
        {
            return (await roleManager.DeleteAsync(role)).Succeeded;
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            return (await roleManager.UpdateAsync(role)).Succeeded;
        }

        
    }
}
