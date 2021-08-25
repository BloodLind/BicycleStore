using BicycleStore.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Models.ViewModels.Account
{
    public class RegistrationViewModel
    {
        public User User { set; get; }

        [DataType(DataType.Password)]
        public string Password { set; get; }
        [DataType(DataType.Password)]

        public string ConfirmPassword { set; get; }
        public bool Authorize { set; get; }
    }
}
