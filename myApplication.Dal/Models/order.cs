using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApplication.Dal.Models
{
  public  class order
    {
        public int id { get; set; }
        public DateTime orderDate { get; set; }
        public int productId { get; set; }
        public string userId { get; set; }

        public product Product { get; set; }
        public applicationUser   ApplicationUser { get; set; }

        public virtual ICollection<orderDetails> OrderDetails { get; set; }


    }
}
