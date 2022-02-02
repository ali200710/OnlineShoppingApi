using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myApplication.Dal.Models;
using myApplication.Dal.ViewModels;

namespace myApplication.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly myAppDbContext context;

        public OrderController(myAppDbContext context)
        {
            this.context = context;
        }


        [HttpPost]
        public async Task<IActionResult> post(IEnumerable<orderViewModel> model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                List<order> createOrder = new List<order>();

                foreach (var item in model)
                {
                    order objOrder = new order();

                    objOrder.orderDate = item.orderDate;
                    objOrder.productId = item.productId;
                    objOrder.userId = item.userId;
                    
                    createOrder.Add(objOrder);
                }

                await context.orders.AddRangeAsync(createOrder);
                await context.SaveChangesAsync();

   

                return Ok(createOrder);
            }
            catch (Exception e)
            {
                return BadRequest(new { e });
            }
        }
    }
}
