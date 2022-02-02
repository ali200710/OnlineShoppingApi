using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApplication.Dal.Models
{
   public class product
    {
        public int id { get; set; }
        public string productName { get; set; }
        public int productPrice { get; set; }

        public string imgUrl { get; set; }


        public virtual ICollection<order> Orders { get; set; }
        public virtual ICollection<orderDetails> OrderDetails { get; set; }


    }
}
