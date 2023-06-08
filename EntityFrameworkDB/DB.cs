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

        public IList<StudentGrade>? StudentGrades { get; set; }

        
    }
    public class Grade
    {
        public int GradeId { get; set; }
        public string? GradeName { get; set; }
        public string? Section { get; set; }

        public IList<StudentGrade>? StudentGrades { get; set; }


    }

    public class StudentGrade
    {
        public int StudentID { get; set; }
        public Student? Student { get; set; }
        public int GradeID { get; set; }
        public Grade? Grade { get; set; }
    }

    public class SchoolContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;Encrypt=false");  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGrade>().HasKey(sg => new {sg.StudentID,sg.GradeID});
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
    }

}
