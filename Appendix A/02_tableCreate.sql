CREATE TABLE Superhero (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(50) NOT NULL,
    Alias nvarchar(50) NOT NULL,
    Origin nvarchar(50) NOT NULL,
);
CREATE TABLE Assistent (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(50) NOT NULL UNIQUE,
    Alias nvarchar(50) NOT NULL,
    Origin nvarchar(50) NOT NULL,
);
CREATE TABLE SuperPower (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(50) NOT NULL,
	Description TEXT NOT NULL,
);