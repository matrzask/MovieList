using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieList.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using MovieList.Models;

namespace MovieList.Models;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MovieListContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<MovieListContext>>()))
        {
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }
            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseYear = 1989,
                    Genre = "Romantic Comedy",
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseYear = 1984,
                    Genre = "Comedy",
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseYear = 1986,
                    Genre = "Comedy",
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseYear = 1959,
                    Genre = "Western",
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    ReleaseYear = 2008,
                    Genre = "Action",
                },
                new Movie
                {
                    Title = "The Dark Knight Rises",
                    ReleaseYear = 2012,
                    Genre = "Action",
                },
                new Movie
                {
                    Title = "Avengers",
                    ReleaseYear = 2012,
                    Genre = "Action",
                }
            );
            
            var adminId = await EnsureUser(serviceProvider, "Admin1.", "admin@admin.com");
            await EnsureRole(serviceProvider, adminId, "User");
            await EnsureRole(serviceProvider, adminId, "Admin");
            
            context.SaveChanges();
        }
    }
    
    private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
        string testUserPw, string UserName)
    {
        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        var user = await userManager.FindByNameAsync(UserName);
        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = UserName,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, testUserPw);
        }

        if (user == null)
        {
            throw new Exception("The password is probably not strong enough!");
        }

        return user.Id;
    }

    private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
        string uid, string role)
    {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

        if (roleManager == null)
        {
            throw new Exception("roleManager null");
        }

        IdentityResult IR;
        if (!await roleManager.RoleExistsAsync(role))
        {
            IR = await roleManager.CreateAsync(new IdentityRole(role));
        }

        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        //if (userManager == null)
        //{
        //    throw new Exception("userManager is null");
        //}

        var user = await userManager.FindByIdAsync(uid);

        if (user == null)
        {
            throw new Exception("The testUserPw password was probably not strong enough!");
        }

        IR = await userManager.AddToRoleAsync(user, role);

        return IR;
    }
}