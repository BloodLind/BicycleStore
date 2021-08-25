using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BicycleStore.Identity.Models;

namespace BicycleStore.Web.Models.ViewModels.Role
{
    public class ChangeRoleViewModel
    {
     
            public string UserId { get; set; }
            public string UserEmail { get; set; }
            public List<BicycleStore.Identity.Models.Role> AllRoles { get; set; }
            public IList<string> UserRoles { get; set; }
            public ChangeRoleViewModel()
            {
                AllRoles = new List<BicycleStore.Identity.Models.Role>();
                UserRoles = new List<string>();
            }
        
    }
}
