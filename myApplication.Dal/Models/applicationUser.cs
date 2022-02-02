using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApplication.Dal.Models
{
   public class applicationUser:IdentityUser
    {
        public int age { get; set; }
    }
}
