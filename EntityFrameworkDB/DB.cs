using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDB
{
    public class Student
    {
        public int StudentID { get; set; }
        public string? StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public Grade? Grade { get; set; }
    }
    public class Grade
    {
        public int GradeId { get; set; }
        public string? GradeName { get; set; }
        public string? Section { get; set; }

        public ICollection<Student>? Students { get; set; }
    }

    public class SchoolContext : DbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;Encrypt=false");  
        }
    }

}
