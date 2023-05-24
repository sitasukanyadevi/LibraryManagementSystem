Create Database LibMS
 
drop database LibMS

use LibMS


Create Table Login_Details
(
Userr_Id varchar(20),
Passwordd varchar(20)
)

Insert into Login_Details values('sukanya','12345'),('seetha','54321')

Create Table Student_Details
(
Student_RollNo int identity Primary key,
Student_Name varchar(50),
Student_Email varchar(50),
Student_Branch varchar(20),
)

select * from Student_Details

Create Table Book_Details
(
Book_ID int identity Primary key,
Book_Name varchar(50),
Book_Author varchar(50),
Available_Stock int
)

drop table Book_Details
 select * from Login_Details

CREATE TABLE Issued_Books
(
Issue_ID int identity Primary key,
Book_ID int,
Student_RollNo int references Student_Details(Student_RollNo),
Issue_Book_Date Date
)

select * from Issued_Books

CREATE TABLE Returned_Books
(
Return_ID int identity Primary Key,
Issue_ID int,
Student_RollNo int,
Book_ID int,
Return_Date date,
)

select * from Student_Details
select * from Book_Details
select * from Issued_Books
select * from Returned_Books

select * from Student_Details
select * from Book_Details
select * from Issued_Books
select * from Returned_Books
select * from Login_Details;