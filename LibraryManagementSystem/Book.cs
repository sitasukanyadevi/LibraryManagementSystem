using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public class Book : IBook
    {
        public SqlConnection con;
        public DataSet ds;
        public SqlDataAdapter student_Adp;
        public SqlDataAdapter book_Adp;
        public SqlDataAdapter issue_Adp;
        public SqlDataAdapter return_Adp;
        public SqlCommandBuilder student_Builder;
        public SqlCommandBuilder book_Builder;
        public SqlCommandBuilder issue_Builder;
        public SqlCommandBuilder return_Builder;

        public Book()
        {
            con = new SqlConnection("Server=US-8ZBJZH3; database=LibMS; Integrated Security=true");
            ds = new DataSet();
            book_Adp = new SqlDataAdapter("SELECT * FROM Book_Details",con);
            issue_Adp = new SqlDataAdapter("SELECT * FROM Issued_Books", con);
            return_Adp = new SqlDataAdapter("SELECT * FROM Returned_Books", con);
            student_Adp = new SqlDataAdapter("SELECT * FROM Student_Details", con);

            student_Builder = new SqlCommandBuilder(student_Adp);          
            book_Builder = new SqlCommandBuilder(book_Adp);
            issue_Builder = new SqlCommandBuilder(issue_Adp);
            return_Builder = new SqlCommandBuilder(return_Adp);

            book_Adp.Fill(ds, "Book_Details");
            issue_Adp.Fill(ds, "Issued_Books");
            return_Adp.Fill(ds, "Returned_Books");
            student_Adp.Fill(ds, "Student_Details");

        }
        public int Add_Book_Details(string book_name, string book_author, int available_stock)
        {
            try
            {
                DataRow row = ds.Tables["Book_Details"].NewRow();
                row["Book_Name"] = book_name;
                row["Book_Author"] = book_author;
                row["Available_Stock"] = available_stock;

                ds.Tables["Book_Details"].Rows.Add(row);
                book_Adp.Update(ds, "Book_Details");
                Console.WriteLine("Student details created successfully");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding book details: " + ex.Message);
                return 0;
            }
        }
        public int Edit_Book_Details(int book_id, string column_name, string value)
        {
            try
            {
                DataRow[] rows = ds.Tables["Book_Details"].Select($"Book_ID = {book_id}");

                if (rows.Length > 0)
                {
                    rows[0][column_name] = value;
                    book_Adp.Update(ds, "Book_Details");
                    Console.WriteLine("Book details saved successfully.");
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }

        public int Delete_Book_Details(int book_id)
        {
            try
            {
                DataRow[] rows = ds.Tables["Book_Details"].Select($"Book_ID = {book_id}");

                if (rows.Length > 0)
                {
                    DataRow dRow = rows[0];
                    DataRow[] iRows = ds.Tables["Issued_Books"].Select($"Book_ID = {book_id}");

                    if (iRows.Length > 0)
                    {
                        Console.WriteLine("Cannot delete the book. The book is currently issued.");
                        return 0;
                    }
                    else
                    {
                        dRow.Delete();
                        book_Adp.Update(ds, "Book_Details");
                        Console.WriteLine("Book deleted successfully.");
                        return 1;
                    }
                }
                else
                {
                    Console.WriteLine("Book not found.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }
        
        public int Issue_Book(int book_id, int student_roll)
        {
            try
            {
                DataRow[] bRows = ds.Tables["Book_Details"].Select($"Book_ID = {book_id}");
                DataRow[] sRows = ds.Tables["Student_Details"].Select($"Student_RollNo = {student_roll}");

                if (bRows.Length > 0 && sRows.Length > 0)
                {
                    DataRow bookRow = bRows[0];
                    int availableStock = Convert.ToInt32(bookRow["Available_Stock"]);

                    if (availableStock > 0)
                    {
                        bool isAlreadyIssued = IsBookAlreadyIssued(book_id, student_roll);
                        if (isAlreadyIssued)
                        {
                            Console.WriteLine("The book is already issued to that student.");
                            return 0;
                        }

                        availableStock--;
                        bookRow["Available_Stock"] = availableStock;

                        DataRow row = ds.Tables["Issued_Books"].NewRow();
                        row["Book_ID"] = book_id;
                        row["Student_RollNo"] = student_roll;
                        row["Issue_Book_Date"] = DateTime.Today;
                        ds.Tables["Issued_Books"].Rows.Add(row);

                        book_Adp.Update(ds, "Book_Details");
                        issue_Adp.Update(ds, "Issued_Books");

                        Console.WriteLine("Book issued successfully.");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("No book stock available for this book.");
                        return 0;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid student roll number or book ID.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }

        private bool IsBookAlreadyIssued(int book_id, int student_roll)
        {
            DataRow[] iRows = ds.Tables["Issued_Books"].Select($"Book_ID = {book_id} AND Student_RollNo = {student_roll}");
            return iRows.Length > 0;
        }

        public int Return_Book(int roll, int book_id)
        {
            try
            {
                DataRow[] Rows = ds.Tables["Issued_Books"].Select($"Student_RollNo = {roll} AND Book_ID = {book_id}");

                if (Rows.Length > 0)
                {
                    DataRow Row = Rows[0];
                    int issueId = Convert.ToInt32(Row["Issue_ID"]);
                    int issuedRollNo = Convert.ToInt32(Row["Student_RollNo"]);
                    int issuedBookId = Convert.ToInt32(Row["Book_ID"]);

                    if (issuedRollNo == roll && issuedBookId == book_id)
                    {
                        DataRow[] sRows = ds.Tables["Student_Details"].Select($"Student_RollNo = {roll}");
                        DataRow[] bRows = ds.Tables["Book_Details"].Select($"Book_Id = {book_id}");

                        if (sRows.Length > 0 && bRows.Length > 0)
                        {
                            DataRow sRow = sRows[0];
                            DataRow bRow = bRows[0];

                            Row.Delete();

                            DataRow rRow = ds.Tables["Returned_Books"].NewRow();
                            rRow["Issue_ID"] = issueId;
                            rRow["Student_RollNo"] = roll;
                            rRow["Book_ID"] = book_id;
                            rRow["Return_Date"] = DateTime.Today;

                            ds.Tables["Returned_Books"].Rows.Add(rRow);

                            // Update the changes in the data source

                            issue_Adp.Update(ds, "Issued_Books");
                            return_Adp.Update(ds, "Returned_Books");

                            // Update the available stock
                            int stock = Convert.ToInt32(bRow["Available_Stock"]) + 1;
                            bRow["Available_Stock"] = stock;

                            book_Adp.Update(ds, "Book_Details");

                            Console.WriteLine("Book returned successfully.");
                            return 1;
                        }
                        else
                        {
                            Console.WriteLine("Invalid student roll number or book ID.");
                            return 0;
                        }
                    }
                    else
                    {
                        Console.WriteLine("The book is not issued to this student.");
                        return 0;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid student roll number or book ID.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }
        public int Search_Book_By_AuthorName(string author_name)
        {
            try
            {
                DataRow[] rows = ds.Tables["Book_Details"].Select($"Book_Author = '{author_name}'");

                if (rows.Length > 0)
                {
                    Console.WriteLine("Books found with the specified author name:");

                    foreach (DataRow row in rows)
                    {
                        Console.WriteLine("Book_id | Book_Title | Book_Author | Available_Stock");
                        Console.WriteLine($"{row["Book_Id"]} | {row["Book_Name"]} | {row["Book_Author"]} | {row["Available_Stock"]}");
                    }

                    return 1;
                }
                else
                {
                    Console.WriteLine("No books found with the specified author name.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }
        public int Students_Having_Books()
        {
            try
            {
                DataRow[] rows = ds.Tables["Issued_Books"].Select($"Student_RollNo IS NOT NULL");
                int count = rows.Length;
                Console.WriteLine($"Total students with books: {count}");
                Console.WriteLine("Issue_Id | Issue_book | Student_RollNo | Issue_Book_Date");
                if (rows.Length > 0)
                {                  
                    foreach (DataRow row in rows)
                    {
                        for (int j = 0; j < ds.Tables["Issued_Books"].Columns.Count; j++)
                        {
                            Console.Write($"{row[j]} | ");
                        }
                        Console.WriteLine();
                    }
                }
                return count;              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }
    }
}

