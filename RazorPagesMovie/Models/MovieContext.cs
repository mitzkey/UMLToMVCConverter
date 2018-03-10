﻿namespace RazorPagesMovie.Models
{
    using Microsoft.EntityFrameworkCore;
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Worker> Worker { get; set; }
    }
}
