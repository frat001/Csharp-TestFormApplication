using DataAcsess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcsess
{
   public class TestDBContext : DbContext
    {
        public DbSet<Personel> Personels { get; set; }
        public DbSet<PersonelDepartman> departmans { get; set; }

        public DbSet<PersonelDepartmanView> PersonelDepartmanViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-S9NN5I7\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
        }
    }
}
