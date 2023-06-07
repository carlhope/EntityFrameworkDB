using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EntityFrameworkDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new SchoolContext())
            {
                DateTime DOB = Convert.ToDateTime(DOBdatepicker.Text);
                var stud = new Student() { StudentName = studentNametextbox.Text, DateOfBirth=DOB };

                ctx.Students.Add(stud);
                ctx.SaveChanges();
                var addedStudentOutputString = " Student " + stud.StudentName + " with the Date of Birth " + stud.DateOfBirth.Value.ToString("dd/MM/yyyy") + " has been successfully added to the database";
                OutputWindow.Text = addedStudentOutputString;
            }
        }

        private void GradeAddButton_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new SchoolContext())
            {
                var grad = new Grade() { GradeName = gradeTextbox.Text, Section=sectionTextBox.Text };
                ctx.Grades.Add(grad);
                ctx.SaveChanges();
                OutputWindow.Text = "grade " + grad.GradeName+" added with section "+grad.Section;
            }
        }

          private void studentQueryButton_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new SchoolContext())
            {
                var student = ctx.Students
                                .Where(s => s.StudentName == studentNametextbox.Text)
                                .FirstOrDefault<Student>();
                if (student != null)
                { OutputWindow.Text = Convert.ToString(student.StudentName); }
                else { OutputWindow.Text ="student "+studentNametextbox.Text+" not found"; }
            }
        }

        private void gradeQueryButton_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new SchoolContext()) 
            {
                var Grade = ctx.Grades
                    .Where(g =>g.GradeName == gradeTextbox.Text)
                    .FirstOrDefault<Grade>();
                if (Grade != null) { OutputWindow.Text = Convert.ToString(Grade.GradeName); }
                else { OutputWindow.Text="grade not found"; }
            }
        }
    }
}
