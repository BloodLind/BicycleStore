﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.Identity.Models
{
    public class UserRole : IdentityUserRole<string>
    {
       
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
