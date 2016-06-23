USE EHR;

CREATE TABLE IF NOT EXISTS prof_PROFCATEGORIES
(
	ProfCatID int NOT NULL,
	Created DateTime NULL,
	IsActive bool NULL,
	Modified DateTime NULL,
	Category varchar(50) NOT NULL,
	Description varchar(128)
);

CREATE TABLE IF NOT EXISTS prof_PROFDEFAULT
(
	ProfID int NOT NULL,
	ProfKey varchar(25) NOT NULL,
	CategoryID int NOT NULL,
	Created DateTime NULL,
	IsActive bool NULL,
	Modified DateTime NULL,
	Val varchar(255) NULL,
	Description varchar(200) NULL,
	UDFID int NULL,
	`Security` int NULL
);

CREATE TABLE IF NOT EXISTS prof_PROFUSERGROUPS
(
	GroupID int NOT NULL,
	Created DateTime NULL,
	IsActive bool NULL,
	Modified DateTime NULL,
	Description varchar(50) NULL
);

CREATE TABLE IF NOT EXISTS prof_ProfGroups
(
	ProfGroupID int NOT NULL,
	CategoryID int NOT NULL,
	ProfDefID int NOT NULL,
	Created DateTime NULL,
	IsActive bool NULL,
	Modified DateTime NULL,
	Val varchar(255) NULL
);

CREATE TABLE IF NOT EXISTS prof_PROFGROUPMEMBERSHIP
(
	ProfGroupMemberID int NOT NULL,
	UserID int NOT NULL,
	GroupID int NULL,
	Created DateTime NULL,
	IsActive bool NULL,
	Modified DateTime NULL
);

CREATE TABLE IF NOT EXISTS prof_PROFUSERS
(
	UserID int NOT NULL,
	CategoryID int NOT NULL,
	ProfID int NOT NULL,
	Created DateTime NULL,
	IsActive bool NULL,
	Modified DateTime NULL,
	Val varchar(255) NULL
); 

