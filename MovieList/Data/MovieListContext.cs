using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieList.Models;

namespace MovieList.Data
{
    public class MovieListContext : IdentityDbContext
    {
        public MovieListContext (DbContextOptions<MovieListContext> options)
            : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<MovieList.Models.Movie> Movie { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<MovieList.Models.ListItem> ListItem { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.ListItems)
                .WithOne(i => i.Movie)
                .HasForeignKey(i => i.MovieId);
        } 
    }
}
