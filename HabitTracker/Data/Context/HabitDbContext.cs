using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HabitTracker.Models;
using System.Reflection;

namespace HabitTracker.Data.Context
{
    public class HabitDbContext(DbContextOptions<HabitDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Habit> Habits { get; set; }
    }
}

