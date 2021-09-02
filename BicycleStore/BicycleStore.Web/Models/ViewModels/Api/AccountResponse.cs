using BicycleStore.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Models.ViewModels.Api
{
    public class AccountResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Role { get; set; }
    }
}
