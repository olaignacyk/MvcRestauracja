using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcRestauracja.Models;

namespace MvcRestauracja.Data
{
    public class MvcRestauracjaContext : DbContext
    {
        public MvcRestauracjaContext (DbContextOptions<MvcRestauracjaContext> options)
            : base(options)
        {
        }
         public DbSet<User> Users { get; set; }
        public DbSet<MvcRestauracja.Models.Danie> Danie { get; set; } = default!;

        public DbSet<MvcRestauracja.Models.Stolik> Stolik { get; set; } = default!;

        public DbSet<MvcRestauracja.Models.Klient> Klient { get; set; } = default!;

        public DbSet<MvcRestauracja.Models.Kelner> Kelner { get; set; } = default!;
        public DbSet<Stolik> Stoliki { get; set; }
        public DbSet<Danie> Dania { get; set; }

    }
}
