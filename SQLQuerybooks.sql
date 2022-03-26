
CREATE TABLE Authors(
Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(100),
Mobile VARCHAR(100),
Email VARCHAR(100)
)

SELECT * FROM Authors;
SELECT * FROM Books;
CREATE TABLE Books(
Id INT PRIMARY KEY IDENTITY,
Title VARCHAR(100),
Price Money,
Quantity INT,
Published_On VARCHAR(100),
Author_Id INT FOREIGN KEY REFERENCES Authors(Id)
)
INSERT INTO Authors(Name,Mobile,Email)
VALUES('Asif',846549169,'asif@gmail.com'),
('Arif',887886455,'ariffuzu001@gmail.com'),
('Sagar',9976574455,'sagar@gmail.com')
INSERT INTO Books (Title,Price,Quantity,Published_On,Author_Id)
VALUES('Python',55,2,GETDATE(),1),
('.Net',525,1,GETDATE(),2),
('Sql',25,3,GETDATE(),2),
('JQuery',5,5,GETDATE(),3),
('Javascript',55,4,GETDATE(),3),
('C-Sharp',55,11,GETDATE(),1)