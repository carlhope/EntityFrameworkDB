using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public String CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            return Regex.Replace(strIn, @"[^\w\.@-]", "");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new SchoolContext())
            {
                var studentTextboxValue = CleanInput(studentNametextbox.Text);
                DateTime DOB = Convert.ToDateTime(DOBdatepicker.Text);
                var stud = new Student() { StudentName = studentTextboxValue, DateOfBirth=DOB };

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
                var gradeNameTextboxvalue = CleanInput(gradeTextbox.Text);
                var sectionTextboxValue=CleanInput(sectionTextBox.Text);
                var grad = new Grade() { GradeName = gradeNameTextboxvalue, Section=sectionTextboxValue };
                ctx.Grades.Add(grad);
                ctx.SaveChanges();
                OutputWindow.Text = "grade " + grad.GradeName+" added with section "+grad.Section;
            }
        }

        private void studentQueryButton_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new SchoolContext())
            {
                var studentNameTextboxValue = CleanInput(studentNametextbox.Text);
                IQueryable<Student> students = 
                                 from s in ctx.Students
                                 where s.StudentName == studentNameTextboxValue
                                select s;

                OutputWindow.Text = "";
                foreach (var student in students)
                {
                    OutputWindow.Text += student.StudentName +Environment.NewLine;
                }
                
            }
        }

        private void gradeQueryButton_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new SchoolContext()) 
            {
                var gradeTextboxValue = CleanInput(gradeTextbox.Text);
                var Grade = ctx.Grades
                    .Where(g =>g.GradeName == gradeTextboxValue)
                    .FirstOrDefault<Grade>();
                if (Grade != null) { OutputWindow.Text = Convert.ToString(Grade.GradeName); }
                else { OutputWindow.Text="grade not found"; }
            }
        }
    }
}
