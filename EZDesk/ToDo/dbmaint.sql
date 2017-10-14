--
-- Table structure for table `ehr`.`todo_lists`
--

DROP TABLE IF EXISTS `todo_lists`;
CREATE TABLE `todo_lists` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'ID for this list',
  `ListName` varchar(100) NOT NULL DEFAULT '' COMMENT 'Name/Description for this list',
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'True the list is deleted, False the list is active',
  `UserID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'User ID this List belongs to',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Table structure for table `ehr`.`todo_tasks`
--

DROP TABLE IF EXISTS `todo_tasks`;
CREATE TABLE `todo_tasks` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'ID for this task',
  `TaskName` varchar(100) NOT NULL DEFAULT '' COMMENT 'Name of the task',
  `Completed` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'True when the user marks the task as complete',
  `TargetDate` datetime DEFAULT NULL COMMENT 'Date/Time the task is scheduled to be completed',
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'True when the task is deleted',
  `ListID` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'The Lists table ID that this task belongs to',
  PRIMARY KEY (`ID`),
  KEY `FK_todo_Tasks_List` (`ListID`),
  CONSTRAINT `FK_todo_Tasks_List` FOREIGN KEY (`ListID`) REFERENCES `todo_lists` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
