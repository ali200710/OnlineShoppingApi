using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myApplication.Dal.Models;
using Microsoft.AspNetCore.Identity;

namespace myApplication.Dal.Helper
{
   public static class seedData
    {
        
        public static void seedProduct(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<product>().HasData(


                new product { id = 1, productName = "watermelon", productPrice = 5, imgUrl = "watermelon.jpg" },
                new product { id = 2, productName = "apple", productPrice = 10, imgUrl = "apple.jpg" },
                new product { id = 3, productName = "banana", productPrice = 8, imgUrl = "banana.jpg" },
                new product { id = 4, productName = "Carrots", productPrice = 3, imgUrl = "Carrots.jpg" },
                new product { id = 5, productName = "rice", productPrice = 7, imgUrl = "rice.jpg" },
                new product { id = 6, productName = "meat", productPrice = 140, imgUrl = "meat.jpg" }


                );
        }


        public static void seedIdentity(this ModelBuilder modelBuilder)
        {
            string admin1Id = "87bb6801-1de7-4ee1-bd47-85560b34c7d0";
            string customerId = "97bb6801-2de8-4ee1-bd47-85560b34c7d5";

            string roleId1 = "121c934f-9351-43b4-a35a-32071864b16c";
            string roleId2 = "84078e89-020e-46e6-8463-d303957bf026";


            var hasher = new PasswordHasher<applicationUser>(null);

            var admin1 = new applicationUser
            {
                Id = admin1Id,
                Email = "a@a",
                UserName = "admin",
                NormalizedUserName = "admin",
                age = 40,
                NormalizedEmail = "a@a",
                PasswordHash = hasher.HashPassword(null, "admin"),
                SecurityStamp = string.Empty,
                EmailConfirmed = true,


            };


            var customer = new applicationUser
            {
                Id = customerId,
                Email = "c@c",
                UserName = "customer",
                NormalizedUserName = "customer",
                age = 40,
                NormalizedEmail = "c@c",
                PasswordHash = hasher.HashPassword(null, "customer"),
                SecurityStamp = string.Empty,
                EmailConfirmed = true,


            };


            modelBuilder.Entity<applicationUser>(a => a.HasData(
                admin1,
                customer
                ));


            modelBuilder.Entity<IdentityRole>(a => a.HasData(
                           new IdentityRole
                           {
                               Id = roleId1,
                               Name = "admin",
                               NormalizedName = "admin"
                           },
                            new IdentityRole
                            {
                                Id = roleId2,
                                Name = "customer",
                                NormalizedName = "customer"
                            }
                           ));




            modelBuilder.Entity<IdentityUserRole<string>>(a => a.HasData(

                new IdentityUserRole<string>
                {
                    RoleId = roleId1,
                    UserId = admin1Id
                },

                new IdentityUserRole<string>
                {
                    RoleId = roleId2,
                    UserId = customerId
                }
                ));


        }

    }
}
