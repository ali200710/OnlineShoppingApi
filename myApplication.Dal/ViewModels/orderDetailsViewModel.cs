using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApplication.Dal.ViewModels
{
   public class orderDetailsViewModel
    {
        public int totalPrice { get; set; }
        public int quantity { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
    }
}
