using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieList.Data;
using System;
using System.Linq;
using MovieList.Models;

namespace MovieList.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
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
            context.SaveChanges();
        }
    }
}