using System;
using System.Data.SqlClient;
using System.Data;
namespace LibraryManagementSystem
{
    class LibraryManagement
    {
        public SqlDataAdapter book_Adp;
        public SqlDataAdapter student_Adp;
        public SqlDataAdapter user_Adp;
        public SqlDataAdapter issue_Adp;
        public SqlDataAdapter return_Adp;
        public DataSet ds;
        public SqlCommandBuilder book_Builder;
        public SqlCommandBuilder student_Builder;
        public SqlCommandBuilder issue_Builder;
        public SqlCommandBuilder return_Builder;

        public LibraryManagement(SqlConnection con)
        {
            book_Adp = new SqlDataAdapter("SELECT * FROM Book_Details", con);
            student_Adp = new SqlDataAdapter("SELECT * FROM Student_Details", con);
            user_Adp = new SqlDataAdapter("SELECT * FROM Login_Details", con);
            issue_Adp = new SqlDataAdapter("SELECT * FROM Issued_Books", con);
            return_Adp = new SqlDataAdapter("SELECT * FROM Returned_Books", con);
            ds = new DataSet();
            book_Builder = new SqlCommandBuilder(book_Adp);
            student_Builder = new SqlCommandBuilder(student_Adp);
            issue_Builder = new SqlCommandBuilder(issue_Adp);
            book_Adp.Fill(ds, "Book_Details");
            student_Adp.Fill(ds, "Student_Details");
            user_Adp.Fill(ds, "Login_Details");
            issue_Adp.Fill(ds, "Issued_Books");
            return_Adp.Fill(ds, "Returned_Books");
        }
        public bool Login()
        {
            Console.WriteLine("You need enter User Id and Password to open Library Management System");
            Console.WriteLine("Enter your user id: ");
            string User_id = Console.ReadLine();

            Console.WriteLine("Enter your password: ");
            string Password = Console.ReadLine();

            if (ds != null && ds.Tables["Login_Details"] != null)
            {
                string q = $"Userr_Id ='{User_id}' AND Passwordd = '{Password}'";
                DataRow[] rows = ds.Tables["Login_Details"].Select(q);

                if (rows.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Add_Student_Details()
        {
            var row = ds.Tables["Student_Details"].NewRow();

            Console.WriteLine("Enter Student name: ");
            row["Student_Name"] = Console.ReadLine();

            Console.WriteLine("Enter Student email: ");
            row["Student_Email"] = Console.ReadLine();

            Console.WriteLine("Enter Student branch: ");
            row["Student_Branch"] = Console.ReadLine();

            ds.Tables["Student_Details"].Rows.Add(row);
            student_Adp.Update(ds, "Student_Details");
            Console.WriteLine("Student details created successfully");
        }

        public void Edit_Student_Details()
        {
            Console.Write("Enter student roll that you want to edit: ");
            int roll = Convert.ToInt32(Console.ReadLine());

            DataRow[] rows = ds.Tables["Student_Details"].Select($"Student_RollNo = {roll}");

            if (rows.Length > 0)
            {

                Console.Write("Enter column name: ");
                string columnName = Console.ReadLine();

                Console.Write("Enter new value that you want to edit: ");
                string value = Console.ReadLine();

                rows[0][columnName] = value;
                student_Adp.Update(ds, "Student_Details");
                Console.WriteLine("Student details saved successfully.");
            }
            else
            {
                Console.WriteLine("No student details found with the specified roll no.");
            }
        }

        /*public void Delete_Student_Details()
        {
            Console.WriteLine("Enter roll no  u want to delete:");
            int Roll = Convert.ToInt32(Console.ReadLine());

            DataRow[] rows = ds.Tables["Student_Details"].Select($"Student_RollNo = {Roll}");

            if (rows.Length > 0)
            {
                rows[0].Delete();

                student_Adp.Update(ds, "Student_Details");
                Console.WriteLine("Student deleted successfully");
            }
            else
            {
                Console.WriteLine("Student not found");
            }
        }
        */

        public void Delete_Student_Details()
        {
            Console.WriteLine("Enter the roll number of the student to delete: ");
            int studentRollNo = Convert.ToInt32(Console.ReadLine());
            // Find the student row to delete
            DataRow[] sRows = ds.Tables["Student_Details"].Select($"Student_RollNo = {studentRollNo}");
            if (sRows.Length > 0)
            {
                DataRow sRow = sRows[0];
                // Check if the student has any associated books
                DataRow[] bookRows = ds.Tables["Book_Details"].Select($"Student_RollNo = {studentRollNo}");
                if (bookRows.Length > 0)
                {
                    Console.WriteLine("Cannot delete the student. The student has associated books.");
                }
                else
                {
                    // Delete the student row
                    sRow.Delete();
                    // Update the database with the changes
                    student_Adp.Update(ds, "Student_Details");
                    Console.WriteLine("Student deleted successfully.");
                }
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        public void Add_Book_Details()
        {
            var row = ds.Tables["Book_Details"].NewRow();

            Console.WriteLine("Enter BookId: ");
            row["Book_Id"] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Book Name: ");
            row["Book_Name"] = Console.ReadLine();

            Console.WriteLine("Enter Book Author: ");
            row["Book_Author"] = Console.ReadLine();

            //Console.WriteLine("Enter StudentId: ");
            //row["Student_RollNo"] = Convert.ToInt32(Console.ReadLine());

            ds.Tables["Book_Details"].Rows.Add(row);

            book_Adp.Update(ds, "Book_Details");
            Console.WriteLine("Book details created successfully");
        }

        public void Edit_Book_Details()
        {
            Console.Write("Enter book id that you want to edit: ");
            int id = Convert.ToInt32(Console.ReadLine());

            DataRow[] rows = ds.Tables["Book_Details"].Select($"Book_Id = {id}");

            if (rows.Length > 0)
            {
                Console.Write("Enter column name: ");
                string columnName = Console.ReadLine();

                Console.Write("Enter new value that you want to edit: ");
                string value = Console.ReadLine();

                rows[0][columnName] = value;
                book_Adp.Update(ds, "Book_Details");
                Console.WriteLine("Book details saved successfully.");
            }
            else
            {
                Console.WriteLine("No book details found with the specified ID.");
            }
        }

        /*public void Delete_Book_Details()
        {
            Console.WriteLine("Enter the book id u want to delete:");
            int BookId = Convert.ToInt32(Console.ReadLine());

            DataRow[] rows = ds.Tables["Book_Details"].Select($"Book_Id = {BookId}");

            if (rows.Length > 0)
            {
                rows[0].Delete();
                book_Adp.Update(ds,"Book_Details");
                Console.WriteLine("Book deleted successfully");
            }
            else
            {
                Console.WriteLine("Book not found");
            }
        }
        */

        public void Delete_Book_Details()
        {
            Console.WriteLine("Enter the book ID of the book to delete: ");
            int bookId = Convert.ToInt32(Console.ReadLine());

            // Check if the book is associated with a student
            DataRow[] issueRows = ds.Tables["Issue_Book"].Select($"Book_Id = {bookId}");
            if (issueRows.Length > 0)
            {
                Console.WriteLine("Cannot delete the book. The book is associated with a student.");
            }
            else
            {
                // Find the book row to delete
                DataRow[] bookRows = ds.Tables["Book_Details"].Select($"Book_Id = {bookId}");
                if (bookRows.Length > 0)
                {
                    DataRow bookRow = bookRows[0];
                    bookRow.Delete();

                    // Update the database with the changes
                    book_Adp.Update(ds, "Book_Details");

                    Console.WriteLine("Book deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
        }



        public void Search_Bookby_AuthorName()
        {
            Console.WriteLine("Enter the author you want to search:");
            string Book_Author = Convert.ToString(Console.ReadLine());
            DataRow[] rows = ds.Tables["Book_Details"].Select($"Book_Author = '{Book_Author}'");
            if (rows.Length > 0)
            {
                //foreach (DataRow row in rows)
                //{
                //   Console.WriteLine("Book_id | Book_Title | Book_Author | Student_RollNo");
                //   Console.WriteLine($"{row["Book_Id"]} | {row["Book_Name"]} | {row["Book_Author"]} | {row["Student_RollNo"]}");
                //}
                foreach (DataRow row in rows)
                {
                    for (int j = 0; j < ds.Tables["Book_Details"].Columns.Count; j++)
                    {
                        Console.Write($"{rows[j]} | ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Book Not Found with {Book_Author}");
            }
        }
        public void Search_Studentby_RollNO()
        {
            Console.WriteLine("Enter the Roll No. u want to search:");
            int Student_Roll = Convert.ToInt32(Console.ReadLine());

            DataRow[] rows = ds.Tables["Student_Details"].Select($"Student_RollNo = {Student_Roll}");

            if (rows.Length > 0)
            {
                //foreach (DataRow row in rows)
                //{
                //       Console.WriteLine("Student_RollNo | Student_Name | Student_Email | Student_Branch ");
                //       Console.WriteLine($"{row["Student_RollNo"]} | {row["Student_Name"]} | {row["Student_Email"]} | {row["Student_Branch"]}");
                //}
                foreach (DataRow row in rows)
                {
                    for (int j = 0; j < ds.Tables["Student_Details"].Columns.Count; j++)
                    {
                        Console.Write($"{row[j]} | ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Student Not Found with {Student_Roll}");
            }
        }
        public void Students_Having_Books()
        {
            DataRow[] rows = ds.Tables["Book_Details"].Select("Student_RollNo IS NOT NULL");
            int count = rows.Length;
            Console.WriteLine($"Total students with books: {count}");
        }
        public void Issue_Book()
        {
            Console.WriteLine("Enter the Student RollNo: ");
            int rollNo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the Book ID: ");
            int bookId = Convert.ToInt32(Console.ReadLine());

            DataRow[] sRows = ds.Tables["Student_Details"].Select($"Student_RollNo = {rollNo}");
            DataRow[] bRows = ds.Tables["Book_Details"].Select($"Book_Id = {bookId}");

            if (sRows.Length > 0 && bRows.Length > 0)
            {
                DataRow studentRow = sRows[0];
                DataRow bookRow = bRows[0];

                if (!bookRow.IsNull("Student_RollNo") && Convert.ToInt32(bookRow["Student_RollNo"]) != rollNo)
                {
                    Console.WriteLine("The book is already issued to another student.");
                }
                else
                {
                    bookRow["Student_RollNo"] = rollNo;

                    DataRow row = ds.Tables["Issued_Books"].NewRow();
                    row["Book_ID"] = bookId;
                    row["Student_RollNo"] = rollNo;
                    row["Issue_Book_Date"] = DateTime.Today;

                    ds.Tables["Issued_Books"].Rows.Add(row);

                    // Update the changes in the data source
                    book_Adp.Update(ds, "Book_Details");
                    issue_Adp.Update(ds, "Issued_Books");

                    Console.WriteLine("Book issued successfully.");
                }
            }
            else
            {
                Console.WriteLine("Invalid student roll number or book ID.");
            }
        }
        public void Return_Book()
        {
            Console.WriteLine("Enter the Student RollNo: ");
            int rollNo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the Book ID: ");
            int bookId = Convert.ToInt32(Console.ReadLine());

            DataRow[] sRows = ds.Tables["Student_Details"].Select($"Student_RollNo = {rollNo}");
            DataRow[] bRows = ds.Tables["Book_Details"].Select($"Book_Id = {bookId}");
            
            if (sRows.Length > 0 && bRows.Length > 0)
            {
                DataRow studentRow = sRows[0];
                DataRow bookRow = bRows[0];

                if (Convert.ToInt32(bookRow["Student_RollNo"]) != rollNo)
                {
                    Console.WriteLine("The book is not issued to the specified student.");
                }
                else
                {
                    bookRow["Student_RollNo"] = DBNull.Value;

                    DataRow[] issuedRows = ds.Tables["Issued_Books"].Select($"Book_ID = {bookId} AND Student_RollNo = {rollNo}");
                    if (issuedRows.Length > 0)
                    {
                        DataRow issuedRow = issuedRows[0];
                        issuedRow.Delete();

                        // Update the changes in the data source
                        book_Adp.Update(ds, "Book_Details");
                        issue_Adp.Update(ds, "Issued_Books");

                        Console.WriteLine("Book returned successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No issued book record found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid student roll number or book ID.");
            }
        }



    }

    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=US-8ZBJZH3; database=LibraryMS; Integrated Security=true");
            LibraryManagement obj = new LibraryManagement(con);

            bool success = obj.Login();

            if (success)
            {
                Console.WriteLine("Login successfull!");
           
                string s = "";

                do
                {
                     Console.WriteLine("-----Welcome to Library Management App----");
                     Console.WriteLine("1. Add student details");
                     Console.WriteLine("2. Edit student details");
                     Console.WriteLine("3. Delete student details");
                     Console.WriteLine("4. Add book details");
                     Console.WriteLine("5. Edit book details");
                     Console.WriteLine("6. Delete Book Details ");
                     Console.WriteLine("7. Search Book by AuthorName");
                     Console.WriteLine("8. Search Student by RollNo");
                     Console.WriteLine("9. students having books");
                     Console.WriteLine("10. Issue Book");
                     Console.WriteLine("11. Return book");

                    int choice = 0;

                     try
                     {
                        Console.WriteLine("Enter your choice: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                     }
                     catch (FormatException)
                     {

                        Console.WriteLine("Enter only numbers.");
                     } 

                     switch (choice)
                     {
                        case  1:
                            {
                                obj.Add_Student_Details();
                                break;
                            }
                        case 2:
                            {
                                obj.Edit_Student_Details();
                                break;
                            }
                        case 3:
                            {
                                obj.Delete_Student_Details();
                                break;
                            }
                        case 4:
                            {
                                obj.Add_Book_Details();
                                break;
                            }
                        case 5:
                            {
                                obj.Edit_Book_Details();
                                break;
                            }
                        case 6:
                            {
                                obj.Delete_Book_Details();
                                break;
                            }
                        case 7:
                            {
                                obj.Search_Bookby_AuthorName();
                                break;
                            }
                        case 8:
                            {
                                obj.Search_Studentby_RollNO();
                                break;
                            }
                        case 9:
                            {
                                obj.Students_Having_Books();
                                break;
                            }
                        case 10:
                            {
                                obj.Issue_Book();
                                break;
                            }
                        case 11:
                            {
                                obj.Return_Book();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid choice");
                                break;
                            }
                
                     }
                      Console.WriteLine("Do you want to continue (y/n)?");
                      s = Console.ReadLine();
                } 
                while (s.ToLower() == "y");
            }
            else
            {
                Console.WriteLine("Authentication failed. Invalid user id or password.");
            }
        }
    }
}
