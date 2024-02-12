-- Create database
CREATE DATABASE Bank;

USE Bank;

-- Create tables
CREATE TABLE Client
(
	ClientId INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(128) NOT NULL,
    SecondName NVARCHAR(10),
    LastName NVARCHAR(128) NOT NULL,
    DateOfBirth DATE NOT NULL,
	CLogin NVARCHAR(128) NOT NULL,
	CPassword NVARCHAR(128) NOT NULL
);

-- Insert example data
INSERT INTO Client (FirstName, SecondName, LastName, DateOfBirth, CLogin, CPassword)
VALUES
    ('John', 'A.', 'Doe', '1990-05-15', 'john.doe@example.com', 'password123'),
    ('Jane', '', 'Smith', '1985-08-22', 'jane.smith@example.com', 'securepass'),
    ('Michael', 'B.', 'Johnson', '1978-12-10', 'michael.johnson@example.com', 'mike@123'),
    ('Emily', '', 'Taylor', '1995-03-28', 'emily.taylor@example.com', 'emily_pass'),
    ('David', 'C.', 'Williams', '1982-06-07', 'david.williams@example.com', 'david82'),
    ('Sarah', '', 'Miller', '1993-09-14', 'sarah.miller@example.com', 'sarah123'),
    ('Daniel', 'R.', 'Brown', '1987-11-03', 'daniel.brown@example.com', 'dbrown'),
    ('Olivia', '', 'Anderson', '1998-02-18', 'olivia.anderson@example.com', 'olivia_pass'),
    ('Brian', 'S.', 'Jones', '1975-04-30', 'brian.jones@example.com', 'brian75'),
    ('Ella', '', 'White', '1991-07-12', 'ella.white@example.com', 'ellawhite');

-- Example select
SELECT *
FROM [dbo].[Client]


