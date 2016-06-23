USE EHR;

CREATE TABLE IF NOT EXISTS ehr_AVAILABLEPROPERTIES
(
	PropID int NOT NULL,
	Created DateTime NULL,
	IsActive bool NULL,
	Modified DateTime NULL,
	PROPERTYNAME varchar(50) NOT NULL,
	DESCRIPTION varchar(256) NULL,
	PROPERTYVALUE varchar(256) NULL,
	VISIBILITY int NOT NULL
);
