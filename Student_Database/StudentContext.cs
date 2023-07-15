using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace Student_Database
{
    internal class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        //private readonly string _path = @"E:\campus\semester 3\c#\Student_Database\student.db";

        //protected override void
        //    OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlite($"Data Source = {_path}");

        protected override void
            OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(@"Data Source = ..\..\..\..\student.db");
    }

}
