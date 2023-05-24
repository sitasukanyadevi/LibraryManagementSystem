﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public interface IStudent
    {
        public int Add_Student_Details(string student_name, string student_email, string student_branch);

        public int Edit_Student_Details(int student_roll, string column_name, string value);

        public int Delete_Student_Details(int student_roll);

        public int Search_Student_By_RollNo(int roll);
    }
}
