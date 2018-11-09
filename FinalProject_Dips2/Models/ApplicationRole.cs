﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_Dips2.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string roleName): base(roleName)
        {
           
        }

        public string Description { get; set; }
    }
}

