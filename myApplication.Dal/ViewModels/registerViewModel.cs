using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApplication.Dal.ViewModels
{
   public class registerViewModel
    {
        [Required]
        public string userName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public int age { get; set; }
    }
}
