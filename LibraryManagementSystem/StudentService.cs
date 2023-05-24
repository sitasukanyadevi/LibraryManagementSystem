using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class StudentService : IStudentService
    {
        private readonly IStudent _students;
        public StudentService(IStudent student)
        {
            _students = student;
        }
        public int Add_Student_Details(string student_name, string student_email, string student_branch)
        {
            return _students.Add_Student_Details(student_name, student_email, student_branch);
        }

        public int Edit_Student_Details(int student_roll, string column_name, string value)
        {
            return _students.Edit_Student_Details(student_roll, column_name, value);
        }
        public int Delete_Student_Details(int student_roll)
        {
            return _students.Delete_Student_Details(student_roll);
        }
        public int Search_Student_By_RollNo(int roll)
        {
            return _students.Search_Student_By_RollNo(roll);
        }

    }
}
