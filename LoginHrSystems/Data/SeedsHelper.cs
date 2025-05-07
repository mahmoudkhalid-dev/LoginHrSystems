using LoginHrSystems.Models.Roles;
using LoginHrSystems.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoginHrSystems.Data
{
    public static class SeedsHelper
    {


        public static async Task Seed(ApplicationDbContext applicationDbContext)
        {
            await CreateUsers(applicationDbContext);
            await CreateRoles(applicationDbContext);
            await CreatePermissions(applicationDbContext);
            await CreateUserRole(applicationDbContext);
        }


        private static async Task CreateUsers(ApplicationDbContext applicationDbContext)
        {

            var adminUser = new User
            {
                Email = "mk.admin@gmail.com",
                UserName = "mkAdmin",
                HashedPassword = "123456@Mm112"
            };

            if (!await applicationDbContext.Users.AnyAsync())
            {
                try
                {
                    applicationDbContext.Database.OpenConnection();
                    await applicationDbContext.Users.AddAsync(adminUser);
                    int dd = await applicationDbContext.SaveChangesAsync();
                }
                finally
                {
                    applicationDbContext.Database.CloseConnection();
                }
            }

        }

        private static async Task CreateRoles(ApplicationDbContext applicationDbContext)
        {

            var roles = new List<Role>
            {
                new Role
                {
                    Name = "Admin",
                },
                new Role
                {
                    Name = "GeneralManager",
                },
                new Role
                {
                    Name = "HR",
                },

            };


            if (!await applicationDbContext.Roles.AnyAsync())
            {
                try
                {
                    applicationDbContext.Database.OpenConnection();
                    await applicationDbContext.Roles.AddRangeAsync(roles);
                    int dd = await applicationDbContext.SaveChangesAsync();
                }
                finally
                {
                    applicationDbContext.Database.CloseConnection();
                }
            }

        }

        private static async Task CreatePermissions(ApplicationDbContext applicationDbContext)
        {

            var permissions = new List<Permission>
            {
                new Permission
                {
                    Name = "AddEmployee",
                },
                new Permission
                {
                    Name = "UpdateEmployee",
                },
                new Permission
                {
                    Name = "DeleteEmployee",
                },
                new Permission
                {
                    Name = "GetAllEmployees",
                },
                new Permission
                {
                    Name = "GetEmployeeDetails",
                }
            };

            if (!await applicationDbContext.Permissions.AnyAsync())
            {
                try
                {
                    applicationDbContext.Database.OpenConnection();
                    await applicationDbContext.Permissions.AddRangeAsync(permissions);
                    int dd = await applicationDbContext.SaveChangesAsync();

                    // adding all permissions to admin role
                    await applicationDbContext.RolePermissions.AddRangeAsync(permissions.Select(p => new RolePermission
                    {
                        PermissionId = p.Id,
                        RoleId = 1
                    }).ToList());
                    dd = await applicationDbContext.SaveChangesAsync();

                    // adding all permissions to admin user
                    await applicationDbContext.UserPermissions.AddRangeAsync(permissions.Select(p => new UserPermission
                    {
                        PermissionId = p.Id,
                        UserId = 1
                    }).ToList());
                    dd = await applicationDbContext.SaveChangesAsync();

                }
                finally
                {
                    applicationDbContext.Database.CloseConnection();
                }
            }
        }

        private static async Task CreateUserRole(ApplicationDbContext applicationDbContext)
        {

            if (!await applicationDbContext.UserRoles.AnyAsync())
            {
                try
                {
                    applicationDbContext.Database.OpenConnection();
                    await applicationDbContext.UserRoles.AddAsync(new UserRole
                    {
                        RoleId = 1,
                        UserId = 1
                    });
                    int dd = await applicationDbContext.SaveChangesAsync();
                }
                finally
                {
                    applicationDbContext.Database.CloseConnection();
                }
            }

        }

    }
}
