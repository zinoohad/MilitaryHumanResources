-- Version 0 - Database creation

CREATE TABLE `ArmoredVessels` (
	`ID`	INTEGER NOT NULL,
	`Name`	TEXT NOT NULL,
	`Capacity`	INTEGER NOT NULL,
	PRIMARY KEY(`ID`)
);

CREATE TABLE `Profession` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Title`	TEXT NOT NULL
);

CREATE TABLE `Role` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Title`	TEXT NOT NULL
);

CREATE TABLE `Rank` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Title`	TEXT NOT NULL
);

CREATE TABLE `Soldier` (
	`ID`	INTEGER NOT NULL,
	`Name`	TEXT NOT NULL,
	`Rank`	INTEGER NOT NULL,
	`Role`	INTEGER NOT NULL,
	`Main_Profession`	INTEGER NOT NULL,
	`Secondary_Profession`	INTEGER NOT NULL,
	`Combat_Inlay`	INTEGER NOT NULL,
	`Date_Of_Birth`	TEXT NOT NULL,
	`Address`	TEXT NOT NULL,
	`Mobile_Number`	TEXT UNIQUE,
	`Home_Number`	TEXT UNIQUE,
	`Email`	TEXT UNIQUE,
	`Notes`	TEXT,
	`Avatar_Path`	TEXT,
	FOREIGN KEY(`Rank`) REFERENCES `Rank`(`ID`),
	FOREIGN KEY(`Combat_Inlay`) REFERENCES `ArmoredVessels`(`ID`),
	FOREIGN KEY(`Main_Profession`) REFERENCES `Profession`(`ID`),
	PRIMARY KEY(`ID`),
	FOREIGN KEY(`Role`) REFERENCES `Role`(`ID`),
	FOREIGN KEY(`Secondary_Profession`) REFERENCES `Profession`(`ID`)
);

CREATE VIEW ViewSoldier
AS
SELECT Soldier.ID AS ID, Soldier.Name AS Name,
Rank.ID AS RankID, Rank.Title AS RankTitle, 
Role.ID AS RoleID, Role.Title AS RoleTitle, 
MainProfession.ID AS MainProfessionID, MainProfession.Title AS MainProfessionTitle, 
SecondaryProfession.ID AS SecondaryProfessionID, SecondaryProfession.Title AS SecondaryProfessionTitle, 
ArmoredVessels.ID AS ArmoredVesselsID, ArmoredVessels.Name AS ArmoredVesselsName, ArmoredVessels.Capacity AS ArmoredVesselsCapacity,
Date_Of_Birth AS DateOfBirth, Address, Mobile_Number AS MobileNumber, Home_Number AS HomeNumber, Email, Notes
FROM Soldier
INNER JOIN Rank ON Rank.ID = Soldier.Rank
INNER JOIN Role ON Role.ID = Soldier.Role
INNER JOIN Profession AS MainProfession ON MainProfession.ID = Soldier.Main_Profession
INNER JOIN Profession AS SecondaryProfession ON SecondaryProfession.ID = Soldier.Secondary_Profession
INNER JOIN ArmoredVessels ON ArmoredVessels.ID = Soldier.Combat_Inlay





