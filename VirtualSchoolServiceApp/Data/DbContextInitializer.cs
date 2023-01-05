﻿using Microsoft.AspNetCore.Identity;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Data
{
    public class DbContextInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
            await roleManager.CreateAsync(new IdentityRole("Student"));
            await roleManager.CreateAsync(new IdentityRole("Teacher"));
            await roleManager.CreateAsync(new IdentityRole("Parent"));
        }

        public static async Task SeedTestAccounts(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            var password = "Qwerty12#@.";

            // Administrator
            await CreateAccount(
                userManager,
                new ApplicationUser
                {
                    UserName = "admin@mail.com",
                    Email = "admin@mail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },
                password,
                "Administrator"
            );

            // Administrator
            await CreateAccount(
                userManager,
                new ApplicationUser
                {
                    UserName = "student",
                    Email = "student@mail.com",
                    FirstName = "Student",
                    LastName = "Student",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },
                password,
                "Student"
            );

        }

        private static async Task CreateAccount(UserManager<ApplicationUser> userManager, ApplicationUser user, string password, string? role)
        {
            var existingUser = await userManager.FindByEmailAsync(user.Email);
            
            if (existingUser == null)
            {
                await userManager.CreateAsync(user, password);
                if (!string.IsNullOrWhiteSpace(role))
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}