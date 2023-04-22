using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Magazin_Bijoux.Models;

namespace Magazin_Bijoux.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Magazin_Bijoux.Models.Product> Product { get; set; }
        public DbSet<Magazin_Bijoux.Models.Category> Category { get; set; }
        public DbSet<Magazin_Bijoux.Models.CartItem> CartItem { get; set; }
    }
}
