USE ehr;

CREATE TABLE IF NOT EXISTS doc_Tabs
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
