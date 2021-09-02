using BicycleStore.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BicycleStore.Web.Services.Interfaces
{
    public interface IJwtGenerator
    {
       string CreateToken(ClaimsIdentity identity, User user);
    }
}
