using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Models.ViewModels.Account
{
    public class LoginViewModel
    {
       
        [EmailAddress, Required]
        public string Email { set; get; }

        
        [DataType(DataType.Password), Required]
        public string Password { set; get; }

        public string ReturnURL { set; get; }
        public bool rememberMe { set; get; } = false;
    }
}
