using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Models.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
