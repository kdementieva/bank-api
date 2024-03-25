-- Create database
CREATE DATABASE Bank;
go
USE Bank;

-- Create tables
CREATE TABLE Client
(
	ClientId INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(128) NOT NULL,
    SecondName NVARCHAR(10) default '',
    LastName NVARCHAR(128) NOT NULL,
    DateOfBirth DATE NOT NULL,
	CLogin NVARCHAR(128) NOT NULL,
	CPassword NVARCHAR(20) NOT NULL,
	verifyCod NVARCHAR(10) NOT NULL,
    IsActive BIT default 0
);

-- Wstawienie 10 klientów do tabeli Client
INSERT INTO Client (FirstName, SecondName, LastName, DateOfBirth, CLogin, CPassword, verifyCod, IsActive)
VALUES
    ('John', 'A.', 'Doe', '1990-05-15', 'john.doe@example.com', 'password123', 'ABC123', 1),
    ('Jane', '', 'Smith', '1985-10-20', 'jane.smith@example.com', 'pass123', 'DEF456', 0),
    ('Alice', 'B.', 'Johnson', '1992-02-28', 'alice.johnson@example.com', 'securepass', 'GHI789', 1),
    ('Bob', 'C.', 'Brown', '1988-07-10', 'bob.brown@example.com', 'qwerty', 'JKL012', 0),
    ('Emily', '', 'Wilson', '1995-04-03', 'emily.wilson@example.com', 'password321', 'MNO345', 1),
    ('Michael', 'D.', 'Taylor', '1993-09-18', 'michael.taylor@example.com', 'abc123xyz', 'PQR678', 0),
    ('Sarah', '', 'Clark', '1987-12-25', 'sarah.clark@example.com', 'letmein', 'STU901', 1),
    ('David', 'E.', 'Martinez', '1991-08-08', 'david.martinez@example.com', 'p@ssw0rd', 'VWX234', 0),
    ('Jessica', '', 'Lee', '1989-06-30', 'jessica.lee@example.com', 'pass1234', 'YZA567', 1),
    ('Matthew', 'F.', 'Anderson', '1994-03-12', 'matthew.anderson@example.com', 'hello123', 'BCD890', 0);
