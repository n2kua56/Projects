USE EHR;

CREATE TABLE per_PersonType 
(
    PersonTypeID int NOT NULL,
    Created DateTime NULL,
    IsActive bool null,
    Description varchar(256) NULL
);

INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(0, '2014-07-26 00:00:00', 1, 'Undefined');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(1, '2014-07-26 00:00:00', 1, 'Doctor');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(2, '2014-07-26 00:00:00', 1, 'Staff');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(3, '2014-07-26 00:00:00', 1, 'Clinical Staff');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(4, '2014-07-26 00:00:00', 1, 'Referring Doctor');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(5, '2014-07-26 00:00:00', 1, 'Company');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(6, '2014-07-26 00:00:00', 1, 'Ext Care Person');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(7, '2014-07-26 00:00:00', 1, 'Lab Doctor');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(8, '2014-07-26 00:00:00', 1, 'Lab Facility');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(9, '2014-07-26 00:00:00', 1, 'Insurance Guarantor');
INSERT INTO per_PersonType(PersonTypeID, Created, IsActive, Description)
	VALUES(10, '2014-07-26 00:00:00', 1, 'Insured Person');

CREATE TABLE per_Person 
(
    PersonID int NOT NULL,
    SharedID varchar(50) NULL,
    SSNO varchar(15) NULL,
    PersonTypeID int NOT NULL,
	Created DateTime NULL,
	IsActive bool NULL,
	Modified DateTime NULL,

    Prefix varchar(50) NULL,
    FirstName varchar(50) NULL,
    MiddleName varchar(50) NULL,
    LastName varchar(50) NULL,
    Suffix varchar(50) NULL,

    BirthDate datetime NULL,
    Sex varchar(20) NULL,
    RaceTypeID int NULL,
    Deceased bool NULL,
    YearDeceased datetime NULL,

    NOTE varchar(512) NULL,
    UDF1 varchar(512) NULL,
    UDF2 varchar(512) NULL,
    UDF3 varchar(512) NULL,
    UDF4 varchar(512) NULL,
    UDF5 varchar(512) NULL,
    UDF6 varchar(512) NULL,
    UDF7 varchar(512) NULL,
    UDF8 varchar(512) NULL,
    UDF9 varchar(512) NULL,
    UDF10 varchar(512) NULL
);

CREATE TABLE per_AddressType 
(
    AddressTypeID int,
    Created DateTime null,
    IsActive bool null,
    Description varchar(128)
);

INSERT INTO per_AddressType(AddressTypeID, Created, IsActive, Description)
	VALUES(0, '2014-07-26 00:00:00', 1, 'Undefined');
INSERT INTO per_AddressType(AddressTypeID, Created, IsActive, Description)
	VALUES(1, '2014-07-26 00:00:00', 1, 'Home');
INSERT INTO per_AddressType(AddressTypeID, Created, IsActive, Description)
	VALUES(2, '2014-07-26 00:00:00', 1, 'Work');
INSERT INTO per_AddressType(AddressTypeID, Created, IsActive, Description)
	VALUES(3, '2014-07-26 00:00:00', 1, 'Business');

CREATE TABLE per_Address 
(
    AddressID int NOT null,
    PersonID int NOT null,
    AddressTypeID int NOT null,
    Created DateTime null,
    Modified DateTime null,
    IsActive bool null,
    Address1 varchar(50),
    Address2 varchar(50),
    City varchar(50),
    State varchar(15),
    Zip varchar(15)
);

CREATE TABLE per_CommunicationType 
(
    CommunicationTypeID int NOT null,
    Created DateTime null,
    IsActive bool null,
    Description varchar(128) null
);

INSERT INTO per_CommunicationType(CommunicationTypeID, Created, IsActive, Description)
	VALUES(0, '2014-07-26 00:00:00', 1, 'Undefined');
INSERT INTO per_CommunicationType(CommunicationTypeID, Created, IsActive, Description)
	VALUES(1, '2014-07-26 00:00:00', 1, 'Home Phone');
INSERT INTO per_CommunicationType(CommunicationTypeID, Created, IsActive, Description)
	VALUES(2, '2014-07-26 00:00:00', 1, 'Cell Phone');
INSERT INTO per_CommunicationType(CommunicationTypeID, Created, IsActive, Description)
	VALUES(3, '2014-07-26 00:00:00', 1, 'Fax');
INSERT INTO per_CommunicationType(CommunicationTypeID, Created, IsActive, Description)
	VALUES(4, '2014-07-26 00:00:00', 1, 'Beeper');
INSERT INTO per_CommunicationType(CommunicationTypeID, Created, IsActive, Description)
	VALUES(5, '2014-07-26 00:00:00', 1, 'Work Phone');
INSERT INTO per_CommunicationType(CommunicationTypeID, Created, IsActive, Description)
	VALUES(6, '2014-07-26 00:00:00', 1, 'EMAIL');

CREATE TABLE per_Communication 
(
    CommunicationID int NOT null,
    PersonID int NOT null,
    CommunicationTypeID int NOT null,
    Created DateTime null,
    Modified DateTime null,
    IsActive bool null,
    CommunicationCode varchar(128)
);

CREATE TABLE per_USERSECURITY
(
	USERSECURITYID int NOT NULL,
	USERNAME varchar(50) NULL,
	PERSONID int NULL,
	USERPASSWORD varchar(60) NULL,
    USERPASSWORDTIME datetime NULL,
	Created DateTime NULL,
	ISACTIVE bool NULL,
	Modified DateTime NULL,

	LASTVIEWEDRELNOTES varchar(128) NULL,

	LOGINCOUNT int NULL,
	LASTLOGIN datetime NULL,

	-- [NurseLicense] [varchar](35) NULL,
	UDF1 varchar(256) NULL,
	UDF2 varchar(256) NULL,
	UDF3 varchar(256) NULL,
	UDF4 varchar(256) NULL,
	UDF5 varchar(256) NULL,
	UDF6 varchar(256) NULL,
	UDF7 varchar(256) NULL,
	UDF8 varchar(256) NULL,
	UDF9 varchar(256) NULL,
	UDF10 varchar(256) NULL
	-- [ISOUTOFOFFICE] [smallint] NULL
);

CREATE TABLE per_DOCTORS
(
	DoctorsID int NOT NULL,
	PersonID int NOT NULL,
	UserID int NOT NULL,
	HASALIAS bool NULL,
	ALIAS varchar(256) NULL,
	Visable bool NULL,
	DEA varchar(35) NULL,
	DoctorLicense varchar(35) NULL,
	NPI char(10) NULL,
	SpecialtyQualifier varchar(256) NULL,
	SpecialtyDesc varchar(256) NULL,
	erxactive bool null
);

CREATE TABLE doc_Tabs
(
	TabsID int NOT NULL,
	Description varchar(50) NULL,
	Created DateTime NULL,
	Modified DateTime NULL,
	IsActive bool NULL,
	IsHidden bool NULL,
	SortSeq int NULL,
	MsgAutoSeal bool NULL,
	DocumentGroupNotes varchar(80) NULL,
	PowerLink bool NULL,
	Documents bool NULL,
	PlugInLink bool NULL,
	IsResultTab bool NULL,
	IsDefaultResultTab bool NULL
)



