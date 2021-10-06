CREATE DATABASE AccountApp

USE AccountApp

CREATE TABLE Accounts (
    Id NUMERIC PRIMARY KEY NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(200) NOT NULL,
    Salt NVARCHAR(200) NOT NULL,
    Username NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    Role NVARCHAR(10),
    -- CreatedAt DATETIME NOT NULL,
    -- UpdatedAt DATETIME NOT NULL,
    LastAttemptIp NVARCHAR(46),
    LastIp NVARCHAR(46),
    Os NVARCHAR(30),
    RegistrationEmail NVARCHAR(100)
)

SELECT * FROM Accounts

DROP TABLE Accounts