using AppPortal.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AppPortal.Data
{
    public class DataContext : IdentityDbContext<User, Role, int>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<Cycle> Cycles => Set<Cycle>();
        public DbSet<Form> Forms => Set<Form>();
        public DbSet<FormQuestion> FormQuestions => Set<FormQuestion>();
        public DbSet<Application> Applications => Set<Application>();
        public DbSet<Answer> Answers => Set<Answer>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    public class User : IdentityUser<int> {
        public List<Application>? Applications { get; set; }
        public List<Role>? Roles { get; set; } = new List<Role>();
    }
    public class Role : IdentityRole<int> {
        public List<User> Users { get; set; }
    }
}