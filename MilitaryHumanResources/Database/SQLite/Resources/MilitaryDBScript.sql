-- Version 0 - Database creation

CREATE TABLE `ArmoredVessels` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Military_ID`	TEXT,
	`Name`	TEXT NOT NULL,
	`Capacity`	INTEGER NOT NULL
);

CREATE TABLE `Profession` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Title`	TEXT NOT NULL,
	`Short_Title`	TEXT
);

CREATE TABLE `Role` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Title`	TEXT NOT NULL
);

CREATE TABLE `Rank` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Title`	TEXT NOT NULL
);

CREATE TABLE `Subunit` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT NOT NULL UNIQUE
);

CREATE TABLE `Soldier` (
	`ID`	INTEGER NOT NULL,
	`Name`	TEXT NOT NULL,
	`Rank`	TEXT NOT NULL,
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
	`Subunit`	INTEGER NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY(`Rank`) REFERENCES `Rank`(`ID`),
	FOREIGN KEY(`Role`) REFERENCES `Role`(`ID`),
	FOREIGN KEY(`Main_Profession`) REFERENCES `Profession`(`ID`),
	FOREIGN KEY(`Secondary_Profession`) REFERENCES `Profession`(`ID`),
	FOREIGN KEY(`Combat_Inlay`) REFERENCES `ArmoredVessels`(`ID`),
	FOREIGN KEY(`Subunit`) REFERENCES `Subunit`(`ID`)
);

CREATE VIEW ViewSoldier
AS
SELECT Soldier.ID AS ID, Soldier.Name AS Name,
Rank.ID AS RankID, Rank.Title AS RankTitle, 
Role.ID AS RoleID, Role.Title AS RoleTitle, 
MainProfession.ID AS MainProfessionID, MainProfession.Title AS MainProfessionTitle, MainProfession.Short_Title AS MainProfessionShortTitle,
SecondaryProfession.ID AS SecondaryProfessionID, SecondaryProfession.Title AS SecondaryProfessionTitle, SecondaryProfession.Short_Title AS SecondaryProfessionShortTitle, 
ArmoredVessels.ID AS ArmoredVesselsID, ArmoredVessels.Military_ID AS ArmoredVesselsMilitaryID, ArmoredVessels.Name AS ArmoredVesselsName, ArmoredVessels.Capacity AS ArmoredVesselsCapacity,
Subunit.ID AS SubunitID, Subunit.Name AS SubunitName,
Date_Of_Birth AS DateOfBirth, Address, Mobile_Number AS MobileNumber, Home_Number AS HomeNumber, Email, Notes
FROM Soldier
INNER JOIN Rank ON Rank.ID = Soldier.Rank
INNER JOIN Role ON Role.ID = Soldier.Role
INNER JOIN Profession AS MainProfession ON MainProfession.ID = Soldier.Main_Profession
INNER JOIN Profession AS SecondaryProfession ON SecondaryProfession.ID = Soldier.Secondary_Profession
INNER JOIN ArmoredVessels ON ArmoredVessels.ID = Soldier.Combat_Inlay
INNER JOIN Subunit ON Subunit.ID = Soldier.Subunit





