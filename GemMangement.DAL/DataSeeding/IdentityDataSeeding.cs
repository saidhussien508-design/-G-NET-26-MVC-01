using GemMangement.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.DataSeeding
{
    public class IdentityDataSeeding
    {
        public static async Task SeedIdentityDataAsync(UserManager<ApplicationUser> usermanger,
            RoleManager<IdentityRole> rolemanger,
             ILogger loger,
            CancellationToken ct = default
           )
        {
            try
            {
                var hasuser = await usermanger.Users.AnyAsync();
                var hasrole = await rolemanger.Roles.AnyAsync();
                if (hasuser && hasrole) return;
                if (!hasrole)
                {
                    var role = new List<IdentityRole>()
                {
                    new IdentityRole("SuperAdmin"),
                      new IdentityRole("Admin"),


                };
                    foreach (var s in role)
                    {
                        if (!await rolemanger.RoleExistsAsync(s.Name))
                        {
                           await rolemanger.CreateAsync(s);
                        }
                    }
                }
                if (!hasuser)
                {
                    var superadmin = new ApplicationUser()
                    {
                        Firstname = "Ahmad ",
                        Lastname = "Maher",
                        UserName = "ahmadmaher",
                        Email = "ahmadmaher@gamail.com",
                        PhoneNumber = "01125896347"

                    };
                   var result= await usermanger.CreateAsync(superadmin,"P@ssw0rd");
                    if (!result.Succeeded) 
                    {
                    foreach(var err in result.Errors)
                        {
                            Console.WriteLine(err.Code+err.Description);
                        }
                        return;
                    }
                    await usermanger.AddToRoleAsync(superadmin,"SuperAdmin");
                    var admin = new ApplicationUser()
                    {
                        Firstname = "mahmoud ",
                        Lastname = "hassan",
                        UserName = "mahmoudhassan",
                        Email = "mahmoudhassan@gamail.com",
                        PhoneNumber = "01175395185"

                    };
                    await usermanger.CreateAsync(admin, "P@ssw0rd");
                    await usermanger.AddToRoleAsync(admin, "Admin");
                }

            }

            catch(Exception ex)
            {
                loger.LogError(ex.Message);
            }


        }
    }
}   
