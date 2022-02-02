using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myApplication.Dal.Models;
using myApplication.Dal.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace myApplication.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly myAppDbContext context;
        private readonly IWebHostEnvironment hostingEnvironment;


        public ProductController(myAppDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;

        }


        [HttpGet()]
        public async Task<IActionResult> getAll()
        {
            try
            {
                var data = await context.products.ToListAsync();
                if (data == null)
                {
                    return NoContent();
                }

                return Ok(data);

            }
            catch (Exception e)
            {
                return BadRequest(new {e });
            }
           

        }
    
    
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {

            try
            {
                
                var data = await context.products.SingleOrDefaultAsync(a => a.id == id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception e)
            {

                return BadRequest(new { e});
            }

        }
    
   
    
        [HttpPost()]
        public async Task<IActionResult> post(productViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }

               


                product createProduct = new product { 
                productName=model.productName,
                productPrice=model.productPrice,
                
                };

               await context.products.AddAsync(createProduct);
                await context.SaveChangesAsync();

                return Ok(createProduct);
            }
            catch (Exception e)
            {

                return BadRequest(new { e});
            }


        }


        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> addImage(int id,IFormFile photo)
        {
            try
            {
                string uniqueFileName = null;
                if (photo != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }


                var product = await context.products.SingleOrDefaultAsync(a => a.id == id);

                product.imgUrl = uniqueFileName;
               await context.SaveChangesAsync();
                return Ok(product);

            }
            catch (Exception e)
            {

                return BadRequest(new { e });
            }
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> put(int id,productViewModel model)
        {
            try
            {
                var data = await context.products.FindAsync(id);
                if (data == null)
                {
                    return NotFound();
                }

                if (model == null)
                {
                    return BadRequest();
                }

                data.productName = model.productName;
                data.productPrice = model.productPrice;

                await context.SaveChangesAsync();

                return Ok(data);
            }
            catch (Exception e)
            {

                return BadRequest(new { e });
            }

        }



        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> editImage(int id, IFormFile photo)
        {
            try
            {

                if (photo == null)
                {
                    return BadRequest();
                }


                   var oldPhoto =await context.products.SingleOrDefaultAsync(a => a.id == id);
                   if (oldPhoto == null)
                   {
                    return NotFound();
                   }

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    string oldFileName = oldPhoto.imgUrl;
                    string oldFilePath = Path.Combine(uploadsFolder, oldFileName);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));

                    oldPhoto.imgUrl = uniqueFileName;
                }

                await context.SaveChangesAsync();

                return Ok(new { message="success"});
            }
            catch (Exception e)
            {

                return BadRequest(new { e });
            }

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var data =await context.products.FindAsync(id);
                if (data != null)
                {
                    context.products.Remove(data);
                    await context.SaveChangesAsync();
                    return Ok(data);
                }

                return NotFound();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> deleteImage(int id)
        {
            try
            {

                var data = await context.products.AsNoTracking().SingleOrDefaultAsync(i => i.id == id);

                if (data == null)
                {
                    return NoContent();
                }


                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");

                string FilePath = Path.Combine(uploadsFolder, data.imgUrl);

                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                    return Ok(new { message="deleted success"});
                }


                return NotFound();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


    }
}
