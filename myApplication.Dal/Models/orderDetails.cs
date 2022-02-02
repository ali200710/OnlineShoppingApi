using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myApplication.Dal.Models
{
   public class orderDetails
    {
        public int id { get; set; }
        public int totalPrice { get; set; }
        public int quantity { get; set; }

        [ForeignKey("Order")]

        public int? orderId { get; set; }


        
       [ForeignKey("Product")]
        public int? productId { get; set; }

        public  order Order { get; set; }


        public  product Product { get; set; }


    }
}
