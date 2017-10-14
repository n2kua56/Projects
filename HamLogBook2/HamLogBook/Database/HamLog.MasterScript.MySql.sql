DROP DATABASE IF EXISTS `hamlog`;                 
CREATE DATABASE IF NOT EXISTS `hamlog`;
USE  `hamlog`;

FLUSH PRIVILEGES;
DROP USER IF EXISTS 'hamlog'@'localhost';
FLUSH PRIVILEGES;
CREATE USER 'hamlog'@'localhost' IDENTIFIED BY 'password';
GRANT ALL ON `hamlog` TO 'hamlog'@'localhost';
FLUSH PRIVILEGES;

/****** Object:  Table [dbo].[AvailableProperties]    Script Date: 4/10/2017 3:18:31 PM ******/
DROP TABLE IF EXISTS `AvailableProperties`;
CREATE TABLE `AvailableProperties` (
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`Name` VARCHAR(50) NOT NULL,
    `Description` VARCHAR(50) NULL,
	`Value` VARCHAR(128) NOT NULL
) ENGINE InnoDB;
FLUSH TABLES `AvailableProperties`;

/****** Object:  Table [dbo].[OtherName]    Script Date: 4/10/2017 5:23:46 PM ******/

/* IF OBJECT_ID('dbo.OtherName', 'U') IS NOT NULL
-- BEGIN
--	IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'OtherData_OtherNameId_FK') AND 
--					parent_object_id = OBJECT_ID(N'OtherData'))
--	BEGIN
--		ALTER TABLE dbo.OtherData DROP CONSTRAINT OtherData_OtherNameId_FK
--	END */

DROP TABLE IF EXISTS `OtherName`;
CREATE TABLE `OtherName` (
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`OtherN` INT NOT NULL,
	`OtherName` VARCHAR(50) NOT NULL,
	`OtherShortName` VARCHAR(10) NOT NULL,
	`DataSource` INT NOT NULL DEFAULT 1
) ENGINE InnoDB;
FLUSH TABLE `OtherName`;

/****** Object:  Table [dbo].[Mode]    Script Date: 4/10/2017 4:43:13 PM ******/
DROP TABLE IF EXISTS `Mode`;
CREATE TABLE `Mode` (
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`Mode` VARCHAR(20) NOT NULL,
	`SortOrder` INT NOT NULL DEFAULT 10
) ENGINE InnoDB;
FLUSH TABLE `Mode`;

/****** Object:  Table [dbo].[Licenses]    Script Date: 4/10/2017 2:46:29 PM ******/
DROP TABLE IF EXISTS `Licenses`;
CREATE TABLE `Licenses`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`License` VARCHAR(20) NOT NULL,
	`SortOrder` INT NOT NULL DEFAULT 10
) ENGINE InnoDB;
FLUSH TABLE `Licenses`;

/****** Object:  Table [dbo].[Band]    Script Date: 4/10/2017 3:20:30 PM ******/
DROP TABLE IF EXISTS `Band`;
CREATE TABLE `Band`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`Band` VARCHAR(10) NOT NULL,
	`SortOrder` INT NOT NULL DEFAULT 10
) ENGINE InnoDB;
FLUSH TABLE `Band`;

/****** Object:  Table [dbo].[BandPlan]    Script Date: 4/10/2017 3:23:02 PM ******/
DROP TABLE IF EXISTS `BandPlan`;
CREATE TABLE `BandPlan`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`BandId` INT NOT NULL,
	`FreqLow` VARCHAR(50) NOT NULL,
	`FreqHi` VARCHAR(50) NOT NULL,
	`Usage` VARCHAR(50) NOT NULL, 
	`SortOrder` INT NOT NULL DEFAULT 10
) ENGINE InnoDB;
FLUSH TABLE `BandPlan`;

/****** Object:  Table [dbo].[Privilages]    Script Date: 4/10/2017 5:17:17 PM ******/
DROP TABLE IF EXISTS `Privilages`;
CREATE TABLE `Privilages`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`BandId` INT NOT NULL,
	`LicenseId` INT NOT NULL,
	`FreqLow` VARCHAR(50) NOT NULL,
	`FreqHi` VARCHAR(50) NOT NULL,
	`Usage` VARCHAR(50) NOT NULL
) ENGINE InnoDB;
FLUSH TABLE `Privilages`;

/****** Object:  Table [dbo].[Countries]    Script Date: 4/10/2017 5:25:53 PM ******/
DROP TABLE IF EXISTS `Countries`;
CREATE TABLE `Countries`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`CountryName` VARCHAR(50) NOT NULL,
	`CountryShortName` VARCHAR(10) NULL,
	`SortOrder` INT NOT NULL DEFAULT 10
) ENGINE InnoDB;
FLUSH TABLE `Countries`;

/****** Object:  Table [dbo].[States]    Script Date: 4/10/2017 5:33:09 PM ******/
DROP TABLE IF EXISTS `States`;
CREATE TABLE `States`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`StateName` VARCHAR(50) NOT NULL,
	`StateShortName` VARCHAR(10) NULL,
	`CountryId` INT NOT NULL,
	`SortOrder` INT NOT NULL DEFAULT 10
) ENGINE InnoDB;
FLUSH TABLE `States`;

/****** Object:  Table [dbo].[County]    Script Date: 4/10/2017 5:38:02 PM ******/
DROP TABLE IF EXISTS `County`;
CREATE TABLE `County`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`CountyName` VARCHAR(50) NOT NULL,
	`StateId` INT NOT NULL,
	`SortOrder` INT NOT NULL DEFAULT 10
) ENGINE InnoDB;
FLUSH TABLE `County`;

/****** Object:  Table [dbo].[HamLog]    Script Date: 4/10/2017 10:07:39 AM ******/
DROP TABLE IF EXISTS `HamLog`;
CREATE TABLE `HamLog`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`ContactNo` INT NULL,
	`DatetimeStart` DATETIME NOT NULL,
	`DateTimeEnd` DATETIME NULL,
	`MyCall` VARCHAR(16) NOT NULL,
	`CallSign` VARCHAR(16) NOT NULL,
	`Name` VARCHAR(50) NULL,
	`CountryId` INT NULL,
	`StateId` INT NULL,
	`CountyId` INT NULL,
	`RstSent` INT NULL,
	`RstRcvd` INT NULL,
	`BandId` INT NOT NULL,
	`Frequency` INT NULL,
	`ModeId` INT NOT NULL,
	`Power` INT NULL,
	`Other` VARCHAR(128) NULL,
	`QslSent` BIT NOT NULL,
	`QSLRcvd` BIT NOT NULL,
	`CommentId` INT NULL
) ENGINE InnoDB;
FLUSH TABLE `HamLog`;

/****** Object:  Table [dbo].[Coments]    Script Date: 4/30/2017 9:55:54 AM ******/
DROP TABLE IF EXISTS `Comments`;
CREATE TABLE `Comments`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`HamLogId` INT NOT NULL,
	`CommentText` VARCHAR(256) NOT NULL
) ENGINE InnoDB;
FLUSH TABLE `Comments`;

/****** Object:  Table [dbo].[Address]    Script Date: 4/30/2017 2:22:46 PM ******/
-- Address NOT in use yet
DROP TABLE IF EXISTS `Address`;
CREATE TABLE `Address`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`Street` VARCHAR(50) NULL,
	`City` VARCHAR(50) NULL,
	`State` VARCHAR(50) NULL,
	`Zip` VARCHAR(10) NULL
) ENGINE InnoDB;
FLUSH TABLE `Address`;

/****** Object:  Table [dbo].[OtherData]    Script Date: 4/10/2017 5:42:04 PM ******/
DROP TABLE IF EXISTS `OtherData`;
CREATE TABLE `OtherData`(
	`Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`OtherNameId` INT NOT NULL,
	`OtherText` VARCHAR(128) NOT NULL,
	`LogId` INT NOT NULL
) ENGINE InnoDB;
FLUSH TABLE `OtherData`;

/* 'Add Countries' */
INSERT INTO Countries (`CountryName`, `CountryShortName`, `SortOrder`)
		VALUES('Make Selection', 'Sel', 0);
INSERT INTO Countries (`CountryName`, `CountryShortName`, `SortOrder`)
		VALUES('United States of America', 'USA', 1);
INSERT INTO Countries (`CountryName`, `SortOrder`)
		VALUES('Canada', 2);

INSERT INTO Countries (`CountryName`) VALUES('Afghanistan'), ('Albania'), ('Algeria'), 
	('Andorra'), ('Angola'), ('Antigua and Barbuda'), ('Argentina'), ('Armenia'), 
	('Aruba'), ('Australia'), ('Austria'), ('Azerbaijan'), ('Bahamas, The'), 
	('Bahrain'), ('Bangladesh'), ('Barbados'), ('Belarus'), ('Belgium'), ('Belize'), 
	('Benin'), ('Bhutan'), ('Bolivia'), ('Bosnia and Herzegovina'), ('Botswana'), 
	('Brazil'), ('Brunei'), ('Bulgaria'), ('Burkina Faso'), ('Burma'), ('Burundi'), 
	('Cambodia'), ('Cameroon'), ('Cabo Verde'), ('Central African Republic'), 
	('Chad'), ('Chile'), ('China'), ('Colombia'), ('Comoros'), ('Congo, Democratic Republic of the'), 
	('Congo, Republic of the'), ('Costa Rica'), ('Cote d''Ivoire'), ('Croatia'), ('Cuba'), 
	('Curacao'), ('Cyprus'), ('Czechia'), ('Denmark'), ('Djibouti'), ('Dominica'), 
	('Dominican Republic'), ('East Timor (see Timor-Leste)'), ('Ecuador'), ('Egypt'), 
	('El Salvador'), ('Equatorial Guinea'), ('Eritrea'), ('Estonia'), ('Ethiopia'), ('Fiji'), 
	('Finland'), ('France'), ('Gabon'), ('Gambia, The'), ('Georgia'), ('Germany'), ('Ghana'), 
	('Greece'), ('Grenada'), ('Guatemala'), ('Guinea'), ('Guinea-Bissau'), ('Guyana'), ('Haiti'), 
	('Holy See'), ('Honduras'), ('Hong Kong'), ('Hungary'), ('Iceland'), ('India'), ('Indonesia'), 
	('Iran'), ('Iraq'), ('Ireland'), ('Israel'), ('Italy'), ('Jamaica'), ('Japan'), ('Jordan'), 
	('Kazakhstan'), ('Kenya'), ('Kiribati'), ('Korea, North'), ('Korea, South'), ('Kosovo'), 
	('Kuwait'), ('Kyrgyzstan'), ('Laos'), ('Latvia'), ('Lebanon'), ('Lesotho'), ('Liberia'), 
	('Libya'), ('Liechtenstein'), ('Lithuania'), ('Luxembourg'), ('Macau'), ('Macedonia'), 
	('Madagascar'), ('Malawi'), ('Malaysia'), ('Maldives'), ('Mali'), ('Malta'), ('Marshall Islands'), 
	('Mauritania'), ('Mauritius'), ('Mexico'), ('Micronesia'), ('Moldova'), ('Monaco'), 
	('Mongolia'), ('Montenegro'), ('Morocco'), ('Mozambique'), ('Namibia'), ('Nauru'), 
	('Nepal'), ('Netherlands'), ('New Zealand'), ('Nicaragua'), ('Niger'), ('Nigeria'), 
	('North Korea'), ('Norway'), ('Oman'), ('Pakistan'), ('Palau'), ('Palestinian Territories'), 
	('Panama'), ('Papua New Guinea'), ('Paraguay'), ('Peru'), ('Philippines'), ('Poland'), 
	('Portugal'), ('Qatar'), ('Romania'), ('Russia'), ('Rwanda'), ('Saint Kitts and Nevis'), 
	('Saint Lucia'), ('Saint Vincent and the Grenadines'), ('Samoa'), ('San Marino'), 
	('Sao Tome and Principe'), ('Saudi Arabia'), ('Senegal'), ('Serbia'), ('Seychelles'), 
	('Sierra Leone'), ('Singapore'), ('Saint Maarten'), ('Slovakia'), ('Slovenia'), 
	('Solomon Islands'), ('Somalia'), ('South Africa'), ('South Korea'), ('South Sudan'), 
	('Spain'), ('Sri Lanka'), ('Sudan'), ('Suriname'), ('Swaziland'), ('Sweden'), ('Switzerland'), 
	('Syria'), ('Taiwan'), ('Tajikistan'), ('Tanzania'), ('Thailand'), ('Timor-Leste'), 
	('Togo'), ('Tonga'), ('Trinidad and Tobago'), ('Tunisia'), ('Turkey'), ('Turkmenistan'), 
	('Tuvalu'), ('Uganda'), ('Ukraine'), ('United Arab Emirates'), ('United Kingdom'), 
	('Uruguay'), ('Uzbekistan'), ('Vanuatu'), ('Venezuela'), ('Vietnam'), ('Yemen'), 
	('Zambia'), ('Zimbabwe');

/* 'Add US States' */
SET @countryId = (SELECT (`Id`) FROM `Countries` WHERE `CountryShortName` = 'USA');
INSERT INTO States (`StateName`, `StateShortName`, `CountryId`)
		VALUES('Make Selection', 'zz', @countryID),('Alabama', 'AL', @countryId), ('Montana', 'MT', @countryId),
	('Alaska', 'AK', @countryId), ('Nebraska', 'NE', @countryId), ('Arizona', 'AZ', @countryId),
	('Nevada', 'NV', @countryId), ('Arkansas', 'AR', @countryId), ('New Hampshire', 'NH', @countryId),
	('California', 'CA', @countryId), ('New Jersey', 'NJ', @countryId), ('Colorado', 'CO', @countryId),
	('New Mexico', 'NM', @countryId), ('Connecticut', 'CT', @countryId), ('New York', 'NY', @countryId),
	('Delaware', 'DE', @countryId), ('North Carolina', 'NC', @countryId), ('Florida', 'FL', @countryId),
	('North Dakota', 'ND', @countryId), ('Georgia', 'GA', @countryId), ('Ohio', 'OH', @countryId),
	('Hawaii', 'HI', @countryId), ('Oklahoma', 'OK', @countryId), ('Idaho', 'ID', @countryId),
	('Oregon', 'OR', @countryId), ('Illinois', 'IL', @countryId), ('Pennsylvania', 'PA', @countryId),
	('Indiana', 'IN', @countryId), ('Rhode Island', 'RI', @countryId), ('Iowa', 'IA', @countryId), 
	('South Carolina', 'SC', @countryId), ('Kansas', 'KS', @countryId), ('South Dakota', 'SD', @countryId),
	('Kentucky', 'KY', @countryId), ('Tennessee', 'TN', @countryId), ('Louisiana', 'LA', @countryId),
	('Texas', 'TX', @countryId), ('Maine', 'ME', @countryId), ('Utah', 'UT', @countryId),
	('Maryland', 'MD', @countryId), ('Vermont', 'VT', @countryId), ('Massachusetts', 'MA', @countryId),
	('Virginia', 'VA', @countryId), ('Michigan', 'MI', @countryId), ('Washington', 'WA', @countryId), 
	('Minnesota', 'MN', @countryId), ('West Virginia', 'WV', @countryId), ('Mississippi', 'MS', @countryId),
	('Wisconsin', 'WI', @countryId), ('Missouri', 'MO', @countryId), ('Wyoming', 'WY', @countryId);

/* 'Add Counties'  */
/* '  NY Counties' */
SET @countryId = (SELECT (`Id`) FROM `Countries` WHERE CountryShortName = 'USA');
SET @stateId = (SELECT (`Id`) FROM `States` WHERE `CountryId`=@countryId AND `StateShortName`='NY');
INSERT INTO `County` (`CountyName`, `StateId`) VALUES('Make Selection', @stateId), ('Albany', @stateId), ('Allegany', @stateId),
	('Bronx', @stateId), ('Broome', @stateId), ('Cattaraugus', @stateId), ('Cayuga', @stateId),
	('Chemung', @stateId), ('Chenango', @stateId), ('Clinton', @stateId), ('Columbia', @stateId),
	('Cortland', @stateId), ('Delaware', @stateId), ('Dutchess', @stateId), ('Erie', @stateId), 
	('Essex', @stateId), ('Franklin', @stateId), ('Fulton', @stateId), ('Genesee', @stateId),
	('Greene', @stateId), ('Hamilton', @stateId), ('Herkimer', @stateId), ('Jefferson', @stateId),
	('Kings', @stateId), ('Lewis', @stateId), ('Livingston', @stateId), ('Madison', @stateId),
	('Monroe', @stateId), ('Montgomery', @stateId), ('Nassau', @stateId), ('New York', @stateId),
	('Niagara', @stateId), ('Oneida', @stateId), ('Onondaga', @stateId), ('Ontario', @stateId),
	('Orange', @stateId), ('Orleans', @stateId), ('Oswego', @stateId), ('Otsego', @stateId), ('Putnam', @stateId),
	('Queens', @stateId), ('Rensselaer', @stateId), ('Richmond', @stateId), ('Rockland', @stateId), 
	('Saint Lawrence', @stateId), ('Saratoga', @stateId), ('Schenectady', @stateId),
	('Schoharie', @stateId), ('Schuyler', @stateId), ('Seneca', @stateId), ('Steuben', @stateId),
	('Suffolk', @stateId), ('Sullivan', @stateId), ('Tioga', @stateId), ('Tompkins', @stateId),
	('Ulster', @stateId), ('Warren', @stateId), ('Washington', @stateId), ('Wayne', @stateId),
	('Westchester', @stateId), ('Wyoming', @stateId), ('Yates', @stateId);


/* '  NJ Counties' */
SET @countryId = (SELECT (`Id`) FROM `Countries` WHERE `CountryShortName` = 'USA');
SET @stateId = (SELECT (`Id`) FROM `States` WHERE `CountryId`=@countryId AND `StateShortName`='NJ');
INSERT INTO `County` (`CountyName`, `StateId`) VALUES('Make Selection', @stateid), ('Atlantic', @stateId), ('Bergen', @stateId),
	('Burlington', @stateId), ('Camden', @stateId), ('Cape May', @stateId), ('Cumberland', @stateId),
	('Essex', @stateId), ('Gloucester', @stateId), ('Hudson', @stateId), ('Hunterdon', @stateId),
	('Mercer', @stateId), ('Middlesex', @stateId), ('Monmouth', @stateId), ('Morris', @stateId),
	('Ocean', @stateId), ('Passaic', @stateId), ('Salem', @stateId), ('Somerset', @stateId),
	('Sussex', @stateId), ('Union', @stateId), ('Warren', @stateId);


/* '  Pennsylvania' */
SET @countryId = (SELECT (`Id`) FROM `Countries` WHERE `CountryShortName` = 'USA');
SET @stateId = (SELECT (`Id`) FROM `States` WHERE `CountryId`=@countryId AND `StateShortName`='PA');
INSERT INTO County (CountyName, StateId) VALUES('Make Selection', @stateId),('Adams', @stateId),
	('Lackawanna', @stateId), ('Allegheny', @stateId), ('Lancaster', @stateId),
	('Armstrong', @stateId), ('Lawrence', @stateId), ('Beaver ', @stateId),
	('Lebanon', @stateId), ('Bedford', @stateId), ('Lehigh', @stateId),
	('Berks', @stateId), ('Luzerne', @stateId), ('Blair', @stateId), ('Lycoming', @stateId),
	('Bradford', @stateId), ('McKean', @stateId), ('Bucks', @stateId), ('Mercer', @stateId),
	('Butler', @stateId), ('Mifflin', @stateId), ('Cambria', @stateId), ('Monroe', @stateId),
	('Cameron', @stateId), ('Montgomery', @stateId), ('Carbon', @stateId), ('Montour', @stateId),
	('Centre', @stateId), ('Northampton', @stateId), ('Chester', @stateId), ('Northumberland', @stateId),
	('Clarion', @stateId), ('Perry', @stateId), ('Clearfield', @stateId), ('Philadelphia', @stateId),
	('Clinton', @stateId), ('Pike', @stateId), ('Columbia', @stateId), ('Potter', @stateId),
	('Crawford', @stateId), ('Schuylkill', @stateId), ('Cumberland', @stateId), ('Snyder', @stateId),
	('Dauphin', @stateId), ('Somerset', @stateId), ('Delaware', @stateId), ('Sullivan', @stateId),
	('Elk', @stateId), ('Susquehanna', @stateId), ('Erie', @stateId), ('Tioga', @stateId),
	('Fayette', @stateId), ('Union', @stateId), ('Forest', @stateId), ('Venango', @stateId),
	('Franklin', @stateId), ('Warren', @stateId), ('Fulton', @stateId), ('Washington', @stateId),
	('Greene', @stateId), ('Wayne', @stateId), ('Huntingdon', @stateId), ('Westmoreland', @stateId),
	('Indiana', @stateId), ('Wyoming', @stateId), ('Jefferson', @stateId),
	('York', @stateId), ('Juniata', @stateId);


/* '  Pennsylvania' */
SET @countryId = (SELECT (`Id`) FROM `Countries` WHERE `CountryShortName` = 'USA');
SET @stateId = (SELECT (`Id`) FROM `States` WHERE `CountryId`=@countryId AND `StateShortName`='CT');
INSERT INTO `County` (`CountyName`, `StateId`) VALUES('Make Selection', @stateId),('Fairfield', @stateId),
	('Hartford', @stateId), ('New Haven', @stateId), ('New London', @stateId),
	('Litchfield', @stateId), ('Middlesex', @stateId), ('Tolland', @stateId),
	('Windham', @stateId);

/* '  Massachusetts - from https://www.zillow.com/browse/homes/ma/' */
SET @countryId = (SELECT (`Id`) FROM `Countries` WHERE `CountryShortName` = 'USA');
SET @stateId = (SELECT (`Id`) FROM `States` WHERE `CountryId`=@countryId AND `StateShortName`='MA');
INSERT INTO County (CountyName, StateId) VALUES('Make Selection', @stateId),('Middlesex', @StateId), 
	('Worcester', @StateId), ('Essex', @StateId), ('Suffolk', @StateId), ('Norfolk', @StateId), 
	('Bristol', @StateId), ('Plymouth', @StateId), ('Hampden', @StateId), ('Barnstable', @StateId), 
	('Hampshire', @StateId), ('Berkshire', @StateId), ('Franklin', @StateId), 
	('Dukes', @StateId), ('Nantucket', @StateId);

/* '  Rhode Island - from https://www.zillow.com/browse/homes/ri/' */
SET @countryId = (SELECT (`Id`) FROM `Countries` WHERE `CountryShortName` = 'USA');
SET @stateId = (SELECT (`Id`) FROM `States` WHERE `CountryId`=@countryId AND `StateShortName`='RI');
INSERT INTO `County` (`CountyName`, `StateId`) VALUES('Make Selection', @stateId),('Providence', @StateId), 
	('Kent', @StateId), ('Washington', @StateId), ('Newport', @StateId), ('Bristol', @StateId);

/* 'Add Modes' */
INSERT INTO `MODE` (`Mode`) VALUES('Make Selection'), ('AM'), ('ATV'), ('AMTOR'), ('C4FM'),
		('CHIP'), ('CLOVER'), ('CONTEST1'), ('CW'), ('D-STAR'),
		('DATA'), ('DOMINO'), ('FAX'), ('FM'), ('FREEDV'), ('FSK31'),
		('FSK441'), ('FSQ'), ('GTOR'), ('HELL'), ('HFSK'), ('ISCAT'),
		('JT4'), ('JT65'), ('JT6M'), ('JT9'), ('MFSK16'), ('MFSK8'),
		('MINIRTTY'), ('MT63'), ('OLIVIA'), ('PACKET'), ('PACTOR'),
		('PAX'), ('PSK10'), ('PSK125'), ('PSK31'), ('PSK63'), ('PSK63F'),
		('PSKAM'), ('PSKFEC31'), ('Q15'), ('ROS'), ('RTTY'), ('RTTYM'),
		('SSB'), ('SSTV'), ('THOR'), ('THROB'), ('VOI'), ('WINMOR'),
		('WSPR');

/* 'Add Bands' */
INSERT INTO `BAND` (`BAND`, `SortOrder`) VALUES('Select', 0), ('2190m', 20),
		('560m', 20), ('160m', 1), ('80m', 2), ('60m', 3),
		('40m', 4), ('30m', 5), ('20m', 6), ('17m', 7),
		('15m', 8), ('12m', 9), ('10m', 10), ('6m', 11),
		('2m', 12), ('1.25m', 13), ('70cm',14), ('33cm',20),
		('23cm', 20), ('13cm', 20), ('9cm', 20), ('6cm', 20),
		('3cm', 20), ('1.25cm', 20), ('1.25cm', 20), ('6mm', 20),
		('4mm', 20), ('2.5mm', 20), ('2mm', 20), ('1mm', 20);

/* 'Licenses' */
INSERT INTO `Licenses` (`License`, `SortOrder`) VALUES('Extra', 1),
	('Advanced', 2), ('General', 3), ('Technician', 4), ('Novice', 5);

/* 'OtherName' */
INSERT INTO `OtherName` (`OtherN`, `OtherName`, `OtherShortName`)
	VALUES(1, 'Other 1', 'Other1'), (2, 'Other 2', 'Other2'), (3, 'Other 3', 'Other3'),
	(4, 'Other 4', 'Other4'), (5, 'Other 5', 'Other5'), (6, 'Other 6', 'Other6'),
	(7, 'Other 7', 'Other7'), (8, 'Other 8', 'Other8'), (9, 'Other 9', 'Other9'),
	(10, 'Other 10', 'Other10');

/* 'AvailableProperties' */
INSERT INTO `AvailableProperties`(`Name`, `Description`, `Value`)
	VALUES('CommonCall', 'Call Sign of Station', ''),
			('CommonCounty', 'County where the station is located', ''),
			('CommonContinent', 'Continent the station is located', ''),
			('CommonLat', 'Latitude where the station is located', ''),
			('CommonLong', 'Longitude where the station is located', ''),
			('CommonOperator', 'Operator Call Sign', ''),
			('CommonInitials', 'Initials of the Operator', ''),
			('ok', 'Always show this page at startup if checked', '0'),
			('AlertNew', '', '0'),
			('AlertNewOnBand', '', '0'),
			('AlertNewOnMode', '', '0'),
			('DateFormate', 'Format for dates', '2'),
			('CBHamCallUser', 'UserId for Ham Call', ''),
			('CBHamCallPass', 'Password for Ham Call', ''),
			('CBHamCallEnabled', 'Ham Call is enabled when checked', '0'),
			('CBQRZUser', 'UserId for QRZ', ''),
			('CBQRZPass', 'Password for QRZ', ''),
			('CBQRZEnabled', 'QRZ enabled when checked', '0'),
			('CBACLogCallEnabled', 'AC Log Call enabled when checked', '0');

/* 'BandPlan - from http://www.arrl.org/band-plan' */
/* '  160m'                                        */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '160m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '1.800 MHz', '2.000 MHz', 'CW', 1),
		(@BandId, '1.800 MHz', '1.810 MHz', 'Digital Modes', 2),
		(@BandId, '1.810 MHz', '1.810 MHz', 'CW QRP', 3),
		(@BandId, '1.843 MHz', '2.000 MHz', 'SSB, SSTV and other wide band modes', 4),
		(@BandId, '1.910 MHz', '1.910 MHz', 'SSB QRP', 5),
		(@BandId, '1.995 MHz', '2.000 MHz', 'Experimental', 6),
		(@BandId, '1.999 MHz', '2.000 MHz', 'Beacons', 7);

/* '  80m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '80m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '3.590 MHz', '3.590 Mhz', 'RTTY/Data DX', 1),
		(@BandId, '3.570 MHz', '3.600 MHz', 'RTTY/Data', 2),
		(@BandId, '3.790 MHz', '3.800 MHz', 'DX Window', 3),
		(@BandId, '3.845 MHz', '3.845 MHZ', 'SSTV', 4),
		(@BandId, '3.885 MHz', '3.885 MHz', 'AM calling frequency', 5);

/* '  60m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '60m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '5.3305 MHz', '5.3305 MHz', 'USB Phone, CW, RTTY and Data', 1),
		(@BandId, '5.3465 MHz', '5.3465 MHz', 'USB Phone, CW, RTTY and Data', 2),
		(@BandId, '5.3570 MHz', '5.3570 MHz', 'USB Phone, CW, RTTY and Data', 3),
		(@BandId, '5.3715 MHz', '5.3715 MHz', 'USB Phone, CW, RTTY and Data', 4),
		(@BandId, '5.4035 MHz', '5.4035 MHz', 'USB Phone, CW, RTTY and Data', 5);

/* '  40m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '40m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '7.040 MHz', '7.040 MHz', 'RTTY/Data DX', 1),
		(@BandId, '7.080 MHz', '7.125 MHz', 'RTTY/Data', 2),
		(@BandId, '7.171 MHz', '7.171 MHZ', 'SSTV', 3),
		(@BandId, '7.290 MHz', '7.290 MHz', 'AM calling frequency', 4);

/* '  30m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '30m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '10.130 MHz', '10.140 MHz', 'RTTY', 1),
		(@BandId, '10.140 MHz', '10.150 MHz', 'Packet', 2);

/* '  20m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '20m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '14.070 MHz', '14.095 MHz', 'RTTY', 1),
		(@BandId, '14.095 MHz', '14.0995MHz', 'Packet', 2),
		(@BandId, '14.100 MHz', '14.100 MHz', 'NCDXF Beacons', 3),
		(@BandId, '14.1005 MHz', '14.112 MHz', 'Packet', 4),
		(@BandId, '14.230 MHz', '14.230 MHz', 'SSTV', 5),
		(@BandId, '14.286 MHz', '14.286 MHz', 'AM calling frequency', 6);

/* '  17m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '17m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '18.100 MHz', '18.105 MHz', 'RTTY', 1),
		(@BandId, '18.105 MHz', '18.110 MHz', 'Packet', 2);

/* '  15m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '15m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '21.070 MHz', '21.110 MHz', 'RTTY/DATA', 1),
		(@BandId, '21.340 MHz', '21.340 MHz', 'SSTV', 2);

/* '  12m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '12m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '24.920 MHz', '24.925 MHz', 'RTTY', 1),
		(@BandId, '24.925 MHz', '24.930 MHz', 'Packet', 2);

/* '  10m' */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '10m');
INSERT INTO `BandPlan`(`BandId`, `FreqLow`, `FreqHi`, `Usage`, `SortOrder`)
	VALUES(@BandId, '28.000 MHz', '28.070 MHz', 'CW', 1),
		(@BandId, '28.070 MHz', '28.150 MHz', 'RTTY', 2),
		(@BandId, '28.150 MHz', '28.190 MHz', 'CW', 3),
		(@BandId, '28.200 MHz', '28.300 MHz', 'Beacons', 4),
		(@BandId, '28.300 MHz', '29.300 MHz', 'Phone', 5),
		(@BandId, '28.680 MHz', '28.680 MHz', 'SSTV', 6),
		(@BandId, '29.000 MHz', '29.200 MHz', 'AM', 7),
		(@BandId, '29.300 MHz', '29.510 MHz', 'Satellite Downlinks', 8),
		(@BandId, '29.520 MHz', '29.590 MHz', 'Repeater Inputs', 9),
		(@BandId, '29.600 MHz', '29.600 MHz', 'FM Simplex', 10),
		(@BandId, '29.610 MHz', '29.700 MHz', 'Repeater Outputs', 11);

/* 'Privilages' */
/* '  160m'     */
SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '160m');
SET @LicenseId = (SELECT `Id` FROM `Licenses` WHERE `License`='Extra');
INSERT INTO `Privilages` (`BandId`, `LicenseId`, `FreqLow`, `FreqHi`, `Usage`)
	VALUES(@BandId, @LicenseId, '1.800 MHz', '2.000 MHz', 'RTTY, phone, image');

SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '160m');
SET @LicenseId = (SELECT `Id` FROM `Licenses` WHERE `License`='Advanced');
INSERT INTO `Privilages` (`BandId`, `LicenseId`, `FreqLow`, `FreqHi`, `Usage`)
	VALUES(@BandId, @LicenseId, '1.800 MHz', '2.000 MHz', 'RTTY, phone, image');

SET @BandId = (SELECT (`Id`) FROM `Band` WHERE `Band` = '160m');
SET @LicenseId = (SELECT `Id` FROM `Licenses` WHERE `License`='General');
INSERT INTO `Privilages` (`BandId`, `LicenseId`, `FreqLow`, `FreqHi`, `Usage`)
	VALUES(@BandId, @LicenseId, '1.800 MHz', '2.000 MHz', 'RTTY, phone, image');

