using AuthServiceIN6BM.Domain.Entities;
using AuthServiceIN6BM.Application.Services;
using AuthServiceIN6BM.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using AuthServiceIN6BM.Persistence.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace AuthServiceIN6BM.Persistence.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDBContext context)
    {
        if (!context.Roles.Any())
        {
            var roles = new List<Role>
            {
                new()
                {
                    Id = UuidGenerator.GenerateRoleId(),
                    Name = RoleConstants.ADMIN_ROLE
                },
                new()
                {
                    Id = UuidGenerator.GenerateRoleId(),
                    Name = RoleConstants.USER_ROLE
                }
            };
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleConstants.ADMIN_ROLE);

            if (adminRole != null)
            {
                var passordHasher = new PasswordHashService();
                var userId = UuidGenerator.GenerateUserId();
                var profileId = UuidGenerator.GenerateUserId();
                var emailId = UuidGenerator.GenerateUserId();
                var userRoleId = UuidGenerator.GenerateUserId();
                var adminUser = new User
                
                {
                    Id = userId,
                    Name = "Admin",
                    SurName = "User",
                    UserName = "admin",
                    Email = "admin@sports.local",
                    Password = passordHasher.HashPassword("Admin1234!"),
                    Status = true,

                    UserProfile = new UserProfile
                    {
                        Id = profileId,
                        UserId = userId,
                        ProfilePicture = string.Empty,
                        Phone = string.Empty
                    },

                    UserEmail = new UserEmail
                    {
                        Id = emailId,
                        UserId = userId,
                        EmailVerified = true,
                        EmailVerificationToken = null,
                        EmailVerificationTokenExpiry = null,

                    },
                    UserRoles =
                    [
                        new UserRole
                        {
                            Id = userRoleId,
                            UserId = userId,
                            RoleId = adminRole.Id
                        }
                    ]
                };
                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}