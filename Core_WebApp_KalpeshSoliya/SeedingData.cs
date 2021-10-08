using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp_KalpeshSoliya
{
    public class SeedingData
    {
        /// <summary>
        /// IConfiguration: Used to Read the appsetting from the appsetting.json file
        /// e.g. ConnectionString (Database, Cache, etc)
        /// </summary>
        /// <param name="configuration"></param>
        public SeedingData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void SeedUsers(UserManager<IdentityUser> userManager, string Email, string Password, string Role,string UserName)
        {
            if (userManager.FindByEmailAsync(Email).Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.Email = Email;
                user.UserName = UserName;
                IdentityResult result = userManager.CreateAsync(user, Password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        Role).Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager, string Role)
        {
            if (!roleManager.RoleExistsAsync(Role).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = Role;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}