using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myApplication.Dal.Helper;
namespace myApplication.Dal.Models
{
  public  class myAppDbContext: IdentityDbContext<applicationUser>
    {
        public myAppDbContext(DbContextOptions<myAppDbContext>options):base(options)
        {

        }

        public DbSet<order> orders { get; set; }
        public DbSet<orderDetails> orderDetails { get; set; }
        public DbSet<product> products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.seedIdentity();
            modelBuilder.seedProduct();

          

        }


    }
}
