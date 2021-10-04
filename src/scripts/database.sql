CREATE DATABASE AccountApp

USE AccountApp

CREATE TABLE Accounts (
    Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(200) NOT NULL,
    Salt NVARCHAR(200) NOT NULL,
    Username NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    Role NVARCHAR(10),
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL
)