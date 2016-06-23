-- Create the tables and meta data for the tables dealing with
-- People. accessible
--   1) Everyone has a Person table entry, the type of that Person entry 
--		is listed in the PersonType table.accessible
--   2) Doctors and Staff of the clinic also have entries in the UserSecurity table.
--   3) Doctors also have entries in the Doctors table.accessible
--   4) The Addresses for a person have been normalized in the Addresses table
--      which have the address type listed in the AddressType table
--   5) The Communication entries (Phone, email, beeper etc) has been normalized in
--      CommunicationType table.accessible
--   6) Other tables like Doctors will be added as necessary.
USE EHR;

--
-- Table structure for table `per_Address`
--

DROP TABLE IF EXISTS `per_Address`;
CREATE TABLE `per_Address` (
  `AddressID` int(11) NOT NULL AUTO_INCREMENT,
  `PersonID` int(11) NOT NULL,
  `AddressTypeID` int(11) NOT NULL,
  `Created` datetime DEFAULT NULL,
  `Modified` datetime DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Address1` varchar(50) DEFAULT NULL,
  `Address2` varchar(50) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `State` varchar(15) DEFAULT NULL,
  `Zip` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`AddressID`),
  KEY `per_Address_per_AddressType_idx` (`AddressTypeID`),
  KEY `per_Address_per_Person_idx` (`PersonID`),
  CONSTRAINT `per_Address_per_AddressType` FOREIGN KEY (`AddressTypeID`) REFERENCES `per_AddressType` (`AddressTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `per_Address_per_Person` FOREIGN KEY (`PersonID`) REFERENCES `per_Person` (`PersonID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Table structure for table `per_AddressType`
--

DROP TABLE IF EXISTS `per_AddressType`;
CREATE TABLE `per_AddressType` (
  `AddressTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `Created` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1' COMMENT 'true when the row is active',
  `Modified` datetime DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`AddressTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `per_AddressType`
--

INSERT INTO `per_AddressType` (`Created`, `Description`) 
	VALUES ('2014-07-26 00:00:00','Undefined'),
		('2014-07-26 00:00:00','Home'),
		('2014-07-26 00:00:00','Work'),
		('2014-07-26 00:00:00','Business');

--
-- Table structure for table `per_Communication`
--

DROP TABLE IF EXISTS `per_Communication`;
CREATE TABLE `per_Communication` (
  `CommunicationID` int(11) NOT NULL AUTO_INCREMENT,
  `PersonID` int(11) NOT NULL,
  `CommunicationTypeID` int(11) NOT NULL,
  `Created` datetime DEFAULT NULL,
  `Modified` datetime DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `CommunicationCode` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`CommunicationID`),
  KEY `per_Communication_per_CommunicationType_idx` (`CommunicationTypeID`),
  KEY `per_Communication_per_Person_idx` (`PersonID`),
  CONSTRAINT `per_Communication_per_CommunicationType` FOREIGN KEY (`CommunicationTypeID`) REFERENCES `per_CommunicationType` (`CommunicationTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `per_Communication_per_Person` FOREIGN KEY (`PersonID`) REFERENCES `per_Person` (`PersonID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Table structure for table `per_CommunicationType`
--

DROP TABLE IF EXISTS `per_CommunicationType`;
CREATE TABLE `per_CommunicationType` (
  `CommunicationTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `Created` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1' COMMENT 'true when the row is active',
  `Modified` datetime DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`CommunicationTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `per_CommunicationType`
--

INSERT INTO `per_CommunicationType` (`Created`, `Description`) 
	VALUES ('2014-07-26 00:00:00','Undefined'),
		('2014-07-26 00:00:00','Home Phone'),
		('2014-07-26 00:00:00','Cell Phone'),
		('2014-07-26 00:00:00','Fax'),
		('2014-07-26 00:00:00','Beeper'),
		('2014-07-26 00:00:00','Work Phone'),
		('2014-07-26 00:00:00','EMAIL');

--
-- Table structure for table `per_Doctors`
--

DROP TABLE IF EXISTS `per_Doctors`;
CREATE TABLE `per_Doctors` (
  `DoctorsID` int(11) NOT NULL AUTO_INCREMENT,
  `PersonID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `HASALIAS` tinyint(1) DEFAULT NULL,
  `ALIAS` varchar(256) DEFAULT NULL,
  `Visable` tinyint(1) DEFAULT NULL,
  `DEA` varchar(35) DEFAULT NULL,
  `DoctorLicense` varchar(35) DEFAULT NULL,
  `NPI` char(10) DEFAULT NULL,
  `SpecialtyQualifier` varchar(256) DEFAULT NULL,
  `SpecialtyDesc` varchar(256) DEFAULT NULL,
  `erxactive` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`DoctorsID`),
  KEY `per_Doctors_per_Person_idx` (`PersonID`),
  KEY `per_Doctors_per_Usersecurity_idx` (`UserID`),
  CONSTRAINT `per_Doctors_per_Person` FOREIGN KEY (`PersonID`) REFERENCES `per_Person` (`PersonID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `per_Doctors_per_Usersecurity` FOREIGN KEY (`UserID`) REFERENCES `per_UserSecurity` (`USERSECURITYID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Table structure for table `per_Person`
--

DROP TABLE IF EXISTS `per_Person`;
CREATE TABLE `per_Person` (
  `PersonID` int(11) NOT NULL AUTO_INCREMENT,
  `SharedID` varchar(50) DEFAULT NULL,
  `SSNO` varchar(15) DEFAULT NULL,
  `PersonTypeID` int(11) NOT NULL,
  `Created` datetime DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Modified` datetime DEFAULT NULL,
  `Prefix` varchar(50) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Suffix` varchar(50) DEFAULT NULL,
  `BirthDate` datetime DEFAULT NULL,
  `Sex` varchar(20) DEFAULT NULL,
  `RaceTypeID` int(11) DEFAULT NULL,
  `Deceased` tinyint(1) DEFAULT NULL,
  `YearDeceased` datetime DEFAULT NULL,
  `NOTE` varchar(512) DEFAULT NULL,
  `UDF1` varchar(512) DEFAULT NULL,
  `UDF2` varchar(512) DEFAULT NULL,
  `UDF3` varchar(512) DEFAULT NULL,
  `UDF4` varchar(512) DEFAULT NULL,
  `UDF5` varchar(512) DEFAULT NULL,
  `UDF6` varchar(512) DEFAULT NULL,
  `UDF7` varchar(512) DEFAULT NULL,
  `UDF8` varchar(512) DEFAULT NULL,
  `UDF9` varchar(512) DEFAULT NULL,
  `UDF10` varchar(512) DEFAULT NULL,
  PRIMARY KEY (`PersonID`),
  KEY `per_Person_per_PersonType_idx` (`PersonTypeID`),
  CONSTRAINT `per_Person_per_PersonType` FOREIGN KEY (`PersonTypeID`) REFERENCES `per_PersonType` (`PersonTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Table structure for table `per_PersonType`
--

DROP TABLE IF EXISTS `per_PersonType`;
CREATE TABLE `per_PersonType` (
  `PersonTypeID` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID for the rows in the per_PersonType table',
  `Created` datetime NOT NULL COMMENT 'Date Time the row was created',
  `IsActive` bit(1) NOT NULL DEFAULT b'1' COMMENT 'true when the row is active',
  `Modified` datetime DEFAULT NULL COMMENT 'Date and time this row was last modified',
  `Description` varchar(256) DEFAULT NULL COMMENT 'Description of this PersonType',
  PRIMARY KEY (`PersonTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COMMENT='Each valid PersonType for entries in the Person table must have an entry here';

--
-- Dumping data for table `per_PersonType`
--

INSERT INTO `per_PersonType` (`Created`, `Description`)
	VALUES ('2014-07-26 00:00:00','Undefined'),
		('2014-07-26 00:00:00','Doctor'),
		('2014-07-26 00:00:00','Staff'),
		('2014-07-26 00:00:00','Clinical Staff'),
		('2014-07-26 00:00:00','Referring Doctor'),
		('2014-07-26 00:00:00','Company'),
		('2014-07-26 00:00:00','Ext Care Person'),
		('2014-07-26 00:00:00','Lab Doctor'),
		('2014-07-26 00:00:00','Lab Facility'),
		('2014-07-26 00:00:00','Insurance Guarantor'),
		('2014-07-26 00:00:00','Insured Person');

--
-- Table structure for table `per_UserSecurity`
--

DROP TABLE IF EXISTS `per_UserSecurity`;
CREATE TABLE `per_UserSecurity` (
  `USERSECURITYID` int(11) NOT NULL AUTO_INCREMENT,
  `USERNAME` varchar(50) DEFAULT NULL,
  `PERSONID` int(11) DEFAULT NULL,
  `USERPASSWORD` varchar(60) DEFAULT NULL,
  `USERPASSWORDTIME` datetime DEFAULT NULL,
  `Created` datetime DEFAULT NULL,
  `ISACTIVE` tinyint(1) DEFAULT NULL,
  `Modified` datetime DEFAULT NULL,
  `LASTVIEWEDRELNOTES` varchar(128) DEFAULT NULL,
  `LOGINCOUNT` int(11) DEFAULT NULL,
  `LASTLOGIN` datetime DEFAULT NULL,
  `UDF1` varchar(256) DEFAULT NULL,
  `UDF2` varchar(256) DEFAULT NULL,
  `UDF3` varchar(256) DEFAULT NULL,
  `UDF4` varchar(256) DEFAULT NULL,
  `UDF5` varchar(256) DEFAULT NULL,
  `UDF6` varchar(256) DEFAULT NULL,
  `UDF7` varchar(256) DEFAULT NULL,
  `UDF8` varchar(256) DEFAULT NULL,
  `UDF9` varchar(256) DEFAULT NULL,
  `UDF10` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`USERSECURITYID`),
  KEY `per_UserSecurity_per_Person_idx` (`PERSONID`),
  CONSTRAINT `per_UserSecurity_per_Person` FOREIGN KEY (`PERSONID`) REFERENCES `per_Person` (`PersonID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
