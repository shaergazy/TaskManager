using Microsoft.EntityFrameworkCore;
using System;
using Common.Enums;
using DAL.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DAL.EF;
using System.Linq;

namespace DAL.Seed
{
    public class DatabaseMigrator
    {
        public static async Task SeedDatabaseAsync(IServiceProvider appServiceProvider)
        {
            await using var scope = appServiceProvider.CreateAsyncScope();
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<DatabaseMigrator>>();
            try
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Migration error");
            }

            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                await SeedRolesAsync(roleManager);
                await SeedAdminAsync(userManager);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "DB seed error");
            }
        }

        private static async Task SeedAdminAsync(UserManager<User> userManager)
        {
            if (await userManager.Users.AllAsync(us => us.Email != "admin@sibers.com"))
            {
                var admin = new User
                {
                    Email = "admin@sibers.com",
                    UserName = "admin@sibers.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+10000000000",
                    PhoneNumberConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin",
                    LockoutEnabled = false,
                };

                await userManager.CreateAsync(admin, "12qw!@QW");
                await userManager.AddToRoleAsync(admin, nameof(RoleType.Admin));
            }

        }

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
            {
                var normalizedRole = role.ToString();
                var dbRole = roleManager.Roles.FirstOrDefault(r => r.NormalizedName == normalizedRole);
                if (dbRole == null)
                {
                    var result = await roleManager.CreateAsync(new Role { Name = role.ToString() });
                    dbRole = roleManager.Roles.FirstOrDefault(r => r.NormalizedName == normalizedRole);
                }
            }
        }
    }
}
