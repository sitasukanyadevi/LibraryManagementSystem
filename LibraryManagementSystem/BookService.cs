using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{  
    public class BookService : IBookService
    {
        private readonly IBook _book;
        public BookService(IBook book)
        {
            _book = book;
        }
        public int Add_Book_Details(string book_name, string book_author, int available_stock)
        {
            return _book.Add_Book_Details(book_name, book_author, available_stock);
        }

        public int Edit_Book_Details(int book_id, string column_name, string value)
        {
            return _book.Edit_Book_Details(book_id, column_name, value);
        }

        public int Delete_Book_Details(int book_id)
        {
            return _book.Delete_Book_Details(book_id);
        }

        public int Issue_Book(int book_id, int student_roll)
        {
            return _book.Issue_Book(book_id, student_roll);
        }

        public int Return_Book(int roll, int book_id)
        {
            return _book.Return_Book(roll, book_id);
        }

        public int Search_Book_By_AuthorName(string author_name)
        {
            return _book.Search_Book_By_AuthorName(author_name);
        }     

        public int Students_Having_Books()
        {
            return _book.Students_Having_Books();
        }

    }
}


