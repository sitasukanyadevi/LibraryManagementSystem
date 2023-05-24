using System.Data;
using System.Data.SqlClient;
namespace LibraryManagementSystem
{
    public class Student: IStudent
    {
        public SqlConnection con;
        public DataSet ds;
        public SqlDataAdapter student_Adp;
        public SqlDataAdapter issue_Adp;
        public SqlCommandBuilder student_Builder;
        public SqlCommandBuilder issue_Builder;
        public Student()
        {
            con = new SqlConnection("Server=US-8ZBJZH3; database=LibMS; Integrated Security=true");
            ds = new DataSet();

            student_Adp = new SqlDataAdapter("SELECT * FROM Student_Details", con);
            issue_Adp = new SqlDataAdapter("SELECT * FROM Issued_Books", con);

            student_Builder = new SqlCommandBuilder(student_Adp);
            issue_Builder = new SqlCommandBuilder(issue_Adp);

            student_Adp.Fill(ds, "Student_Details");
            issue_Adp.Fill(ds, "Issued_Books");
        }
        public int Add_Student_Details(string student_name, string student_email, string student_branch)
        {
            try
            {
                var row = ds.Tables["Student_Details"].NewRow();

                row["Student_Name"] = student_name;
                row["Student_Email"] = student_email;
                row["Student_Branch"] = student_branch;

                ds.Tables["Student_Details"].Rows.Add(row);
                student_Adp.Update(ds, "Student_Details");

                Console.WriteLine("Student details created successfully");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding student details: " + ex.Message);
                return 0;
            }
        }
        public int Edit_Student_Details(int student_roll, string column_name, string value)
        {
            try
            {
                DataRow[] rows = ds.Tables["Student_Details"].Select($"Student_RollNo = {student_roll}");

                if (rows.Length > 0)
                {
                    rows[0][column_name] = value;
                    student_Adp.Update(ds, "Student_Details");
                    Console.WriteLine("Student details saved successfully.");
                    return 1;
                }
                else
                {
                    Console.WriteLine("No student details found with the specified roll no.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }
        public int Delete_Student_Details(int student_roll)
        {
            try
            {
                DataRow[] sRows = ds.Tables["Student_Details"].Select($"Student_RollNo = {student_roll}");

                if (sRows.Length > 0)
                {
                    DataRow sRow = sRows[0];
                    DataRow[] bRows = ds.Tables["Issued_Books"].Select($"Student_RollNo = {student_roll}");

                    if (bRows.Length > 0)
                    {
                        Console.WriteLine("Cannot delete the student. The student has associated books.");
                        return 0;
                    }
                    else
                    {
                        sRow.Delete();
                        student_Adp.Update(ds, "Student_Details");
                        Console.WriteLine("Student deleted successfully.");
                        return 1;
                    }
                }
                else
                {
                    Console.WriteLine("Student not found.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }
        public int Search_Student_By_RollNo(int roll)
        {
            try
            {
                DataRow[] rows = ds.Tables["Student_Details"].Select($"Student_RollNo = {roll}");

                Console.WriteLine("Student_RollNo | Student_Name | Student_Email | Student_Branch");

                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        for (int j = 0; j < ds.Tables["Student_Details"].Columns.Count; j++)
                        {
                            Console.Write($"{row[j]} | ");
                        }
                        Console.WriteLine();
                    }

                    return 1;
                }
                else
                {
                    Console.WriteLine("No student found with the specified roll number.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }
    }
}
