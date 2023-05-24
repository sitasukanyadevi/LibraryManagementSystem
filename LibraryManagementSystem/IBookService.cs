using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public interface IBookService
    {
        int Add_Book_Details(string book_name, string book_author, int available_stock);

        int Edit_Book_Details(int book_id, string column_name, string value);

        int Delete_Book_Details(int book_id);

        int Issue_Book(int book_id, int student_roll);

        int Return_Book(int roll, int book_id);

        int Search_Book_By_AuthorName(string author_name);

        int Students_Having_Books();
    }
}
