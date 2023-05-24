using LibraryManagementSystem;
using Moq;
namespace LMS.Tests
{
    public class UnitTest1
    {
        private Login l;
        private Book b;
        private Student s;

        [OneTimeSetUp]
        public void Setup()
        {
            l = new Login();
            b = new Book();
            s = new Student();
        }

        [Test]
        public void Valid_Login_Credentials_ReturnsTrue()
        {
            // Arrange
            string UserId = "sukanya";
            string Password = "12345";

            // Act
            bool result = l.GetLogin(UserId, Password);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Invalid_Login_Credentials_ReturnsFalse()
        {
            // Arrange
            string UserId = "sukanay";
            string Password = "1234";

            // Act
            bool result = l.GetLogin(UserId, Password);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Search_RollNumber_WithExisting_StudentDetails()
        {
            // Arrange
            int RollNumber = 1; // assuming rollno. 1 is in student details

            // Act
            int result = s.Search_Student_By_RollNo(RollNumber);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Search_AuthorName_WithExisting_BooksDetails()
        {
            // Arrange
            string AuthorName = "james"; //assuming authorname is in table

            // Act
            int result = b.Search_Book_By_AuthorName(AuthorName);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]

        public void Students_Having_Books_Count()
        {
              
            int result = b.Students_Having_Books();
            // Assert
            Assert.That(result, Is.EqualTo(3)); // assuming one student having books now
        }


        [Test]
        public void AddBookDetails_WhenRecordInserted_ReturnsNoOfRowsAffected()
        {
            //Arrange
            var book = new Mock<IBook>();
            book.Setup(x => x.Add_Book_Details("Harry Potter", "Author",7)).Returns(1);

            var bservice = new BookService(book.Object);
            //Act
            var result = bservice.Add_Book_Details("Harry Potter", "Author", 7);
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void EditBookDetails_WhenRecordEdited_ReturnsNoOfRowsAffected()
        {
            //Arrange
            var book = new Mock<IBook>();
            book.Setup(x => x.Edit_Book_Details(1,"Book_Name", "c#")).Returns(1);
            var bservice = new BookService(book.Object);

            //Act
            var result = bservice.Edit_Book_Details(1,"Book_Name", "c#");
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void DeleteBookDetails_WhenRecordDeleted_ReturnsNoOfRowsAffected()
        {
            //Arrange

            var book = new Mock<IBook>();
            book.Setup(x => x.Delete_Book_Details(1)).Returns(1);
            var bservice = new BookService(book.Object);

            //Act
            var result = bservice.Delete_Book_Details(1);
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddStudentDetails_WhenRecordInserted_ReturnsNoOfRowsAffected()
        {
            //Arrange
            var student = new Mock<IStudent>();
            student.Setup(x => x.Add_Student_Details("Sukanya", "Sukanya@gmail.com", "cse")).Returns(1);
            var sservice = new StudentService(student.Object);

            //Act
            var result = sservice.Add_Student_Details("Sukanya", "Sukanya@gmail.com", "cse");
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void EditStudentDetails_WhenRecordEdited_ReturnsNoOfRowsAffected()
        {
            //Arrange
            var student = new Mock<IStudent>();
            student.Setup(x => x.Edit_Student_Details(1, "Student_Name", "sukanyadevi")).Returns(1);
            var sservice = new StudentService(student.Object);

            //Act
            var result = sservice.Edit_Student_Details(1, "Student_Name", "sukanyadevi");
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void DeleteStudentDetails_WhenRecordDeleted_ReturnsNoOfRowsAffected()
        {
            //Arrange

            var student = new Mock<IStudent>();
            student.Setup(x => x.Delete_Student_Details(1)).Returns(1);
            var sservice = new StudentService(student.Object);

            //Act
            var result = sservice.Delete_Student_Details(1);
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

    }
}