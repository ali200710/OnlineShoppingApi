using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myApplication.Dal.Models;
using myApplication.Dal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApplication.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {

        private readonly myAppDbContext context;

        public OrderDetailsController(myAppDbContext context)
        {
            this.context = context;
        }


        [HttpPost]
        public async Task<IActionResult> post(IEnumerable<orderDetailsViewModel> model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                List<orderDetails> createOrderDetails = new List<orderDetails>();

                foreach (var item in model)
                {
                    orderDetails objOrderDetails = new orderDetails();

                    objOrderDetails.totalPrice = item.totalPrice;
                    objOrderDetails.orderId = item.orderId;
                    objOrderDetails.quantity = item.quantity;
                    objOrderDetails.productId = item.productId;

                    createOrderDetails.Add(objOrderDetails);
                }

                await context.orderDetails.AddRangeAsync(createOrderDetails);
                await context.SaveChangesAsync();



                return Ok(createOrderDetails);
            }
            catch (Exception e)
            {
                return BadRequest(new { e });
            }
        }

    }
}
