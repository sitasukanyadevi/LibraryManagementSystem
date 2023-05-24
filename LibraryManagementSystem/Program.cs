using System.Data;
using System.Data.SqlClient;
namespace LibraryManagementSystem
{   
      class Program
        {
            static void Main(string[] args)
            {            

                SqlConnection con = new SqlConnection("Server=US-8ZBJZH3; database=LibMS; Integrated Security=true");

                // Creating an instances
                Student obj1 = new Student();
                Book obj2 = new Book();
                Console.WriteLine("Enter Login Details");
                Console.WriteLine("Enter your user ID: ");
                string user_id = Console.ReadLine();
                Console.WriteLine("Enter your password: ");
                string password = Console.ReadLine();
                Login log = new Login();
                bool Success = log.GetLogin(user_id, password);

                if (Success)
                {
                    Console.WriteLine("Login Successfull!");
                    Console.WriteLine();
                    string s = "";
                    do
                    {

                        Console.WriteLine("-----Welcome to Library Management App----");
                        Console.WriteLine("1. Add Student Details");
                        Console.WriteLine("2. Edit Student Details");
                        Console.WriteLine("3. Delete Student Details");
                        Console.WriteLine("4. Add Book Details");
                        Console.WriteLine("5. Edit Book Details");
                        Console.WriteLine("6. Delete Book Details");
                        Console.WriteLine("7. Issue Book");
                        Console.WriteLine("8. Return Book");
                        Console.WriteLine("9. Search Book by Author Name");
                        Console.WriteLine("10. Search Student by Roll Number");
                        Console.WriteLine("11. Students having books");
                        Console.WriteLine();

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
                            case 1:
                                {
                                    Console.WriteLine("Add Student Details");
                                    Console.WriteLine("Enter Student Name:");
                                    string student_name = Console.ReadLine();
                                    Console.WriteLine("Enter Student Email: ");
                                    string student_email = Console.ReadLine();
                                    Console.WriteLine("Enter Student Branch: ");
                                    string student_branch = Console.ReadLine();
                                    int result = obj1.Add_Student_Details(student_name, student_email, student_branch);

                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("Edit Student Details");
                                    Console.Write("Enter Student Roll No: ");
                                    int student_roll = Convert.ToInt16(Console.ReadLine());
                                    Console.Write("Enter Column Name: ");
                                    string column_name = Console.ReadLine();
                                    Console.Write("Enter New Value: ");
                                    string value = Console.ReadLine();
                                    int result = obj1.Edit_Student_Details(student_roll, column_name, value);
     
                                    break;
                                }

                            case 3:
                                {
                                    Console.WriteLine("Delete Student Details");
                                    Console.Write("Enter Student Roll No: ");
                                    int student_roll = Convert.ToInt16(Console.ReadLine());
                                    int result = obj1.Delete_Student_Details(student_roll);
                                    
                                    break;
                                }

                            case 4:
                                {
                                    Console.WriteLine("Add Book Details");
                                    Console.Write("Enter Book Name: ");
                                    string Book_Name = Console.ReadLine();
                                    Console.Write("Enter Book author : ");
                                    string Book_Author = Console.ReadLine();
                                    Console.Write("Enter Available_Stock: ");
                                    int Available_Stock = Convert.ToInt16(Console.ReadLine());
                                    int result = obj2.Add_Book_Details(Book_Name, Book_Author, Available_Stock);
                                  
                                    break;
                                }
                            case 5:
                                {
                                    Console.WriteLine("Edit Book Details");
                                    Console.Write("Enter Book ID: ");
                                    int book_id = Convert.ToInt16(Console.ReadLine());
                                    Console.Write("Enter Column Name: ");
                                    string book_columnname = Console.ReadLine();
                                    Console.Write("Enter New Value: ");
                                    string book_value = Console.ReadLine();
                                    int result = obj2.Edit_Book_Details(book_id, book_columnname, book_value);

                                    break;
                                }

                            case 6:
                                {
                                    Console.WriteLine("Delete Book Details");
                                    Console.Write("Enter Book ID: ");
                                    int book_id = Convert.ToInt16(Console.ReadLine());
                                    int result = obj2.Delete_Book_Details(book_id);
                                    
                                    break;
                                }

                            case 7:
                                {
                                    Console.Write("Enter Book ID to issue: ");
                                    int book_id = Convert.ToInt16(Console.ReadLine());
                                    Console.Write("Enter Student Roll No: ");
                                    int student_roll = Convert.ToInt16(Console.ReadLine());
                                    int result = obj2.Issue_Book(book_id, student_roll);                                  

                                    break;
                                }

                            case 8:
                                {
                                    Console.WriteLine("Enter the Student RollNo: ");
                                    int roll = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter Book ID to return: ");
                                    int book_id = Convert.ToInt16(Console.ReadLine());
                                    int result = obj2.Return_Book(roll, book_id);
                                 
                                    break;
                                }

                            case 9:
                                {
                                    Console.WriteLine("Search Book by Author Name");
                                    Console.Write("Enter author name / book author: ");
                                    string author_name = Console.ReadLine();
                                    int result = obj2.Search_Book_By_AuthorName(author_name);
                                 
                                    break;
                                }
                            case 10:
                                {
                                    Console.WriteLine("Search Student by Roll Number");
                                    Console.Write("Enter student roll number: ");
                                    int roll = Convert.ToInt32(Console.ReadLine());
                                    int result = obj1.Search_Student_By_RollNo(roll);
                                    
                                    break;
                                }

                            case 11:
                                {
                                    int Count = obj2.Students_Having_Books();                                  
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Invalid choice. Please try again.");
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