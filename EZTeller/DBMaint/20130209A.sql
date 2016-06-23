/* ********************************************************************* */
/*                                                                       */
/* File: Logging.sql                                                     */
/*                                                                       */
/* Add the logging table and triggers to add logging to EZTeller2        */
/*  *
/*                                                                       */
/* ********************************************************************* */

DELIMITER $$

USE Church$$

/* ******************************************************* */
/*                                                         */
/* Create the Logging table.                               */
/*                                                         */
/* ******************************************************* */

DROP TABLE IF EXISTS `ChurchLogging` $$

CREATE TABLE `ChurchLogging` (
  `log_id`    INT NOT NULL AUTO_INCREMENT PRIMARY KEY ,
  `log_table` VARCHAR(30) NOT NULL,
  `table_id`  INT NOT NULL,
  `field`     VARCHAR(50) NOT NULL,
  `old_value` VARCHAR(512),
  `new_value` VARCHAR(512) NOT NULL ,
  `modified`  DATETIME NOT NULL,
  `action`    VARCHAR(20) NOT NULL
) ENGINE = MYISAM $$

/* ******************************************************* */
/*                                                         */
/* Create the triggers for the contribution table. On      */
/* UPDATE save the old and new values IF they have been    */
/* changed. On INSERT log all the NEW values.              */
/*                                                         */
/* ******************************************************* */

DROP TRIGGER IF EXISTS `update_contribution`$$

CREATE TRIGGER `update_contribution` AFTER UPDATE on `contribution`
FOR EACH ROW
BEGIN

    /* cntrbKey is not checked because it is the primary key, autoseq. */

    IF (NEW.cntrbDate != OLD.cntrbDate) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbDate',
           DATE_FORMAT(OLD.cntrbDate, '%Y-%m-%d %H:%i:%s.%f'),
           DATE_FORMAT(NEW.cntrbDate, '%Y-%m-%d %H:%i:%s.%f'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbBatch != OLD.cntrbBatch) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbBatch',
           IFNULL(CAST(OLD.cntrbBatch AS CHAR), 'null'),
           IFNULL(CAST(NEW.cntrbBatch AS CHAR), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbSeq != OLD.cntrbSeq) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbSeq',
           IFNULL(CAST(OLD.cntrbSeq AS CHAR), 'null'),
           IFNULL(CAST(NEW.cntrbSeq AS CHAR), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbPplKey != OLD.cntrbPplKey) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbPplKey',
           IFNULL(CAST(OLD.cntrbPplKey AS CHAR), 'null'),
           IFNULL(CAST(NEW.cntrbPplKey AS CHAR), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbGeneral != OLD.cntrbGeneral) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbGeneral',
           IFNULL(CAST(OLD.cntrbGeneral AS CHAR), 'null'),
           IFNULL(CAST(NEW.cntrbGeneral AS CHAR), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbBuilding != OLD.cntrbBuilding) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbBuilding',
           IFNULL(CAST(OLD.cntrbBuilding AS CHAR), 'null'),
           IFNULL(CAST(NEW.cntrbBuilding AS CHAR), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbMissions != OLD.cntrbMissions) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbMissions',
           IFNULL(CAST(OLD.cntrbMissions AS CHAR), 'null'),
           IFNULL(CAST(NEW.cntrbMissions AS CHAR), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbDesignated != OLD.cntrbDesignated) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbDesignated',
           IFNULL(CAST(OLD.cntrbDesignated AS CHAR), 'null'),
           IFNULL(CAST(NEW.cntrbDesignated AS CHAR), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbType != OLD.cntrbType) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbType',
           IFNULL(OLD.cntrbType, 'null'), IFNULL(NEW.cntrbType, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbComments != OLD.cntrbComments) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbComments',
           IFNULL(OLD.cntrbComments, 'null'), IFNULL(NEW.cntrbComments, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbCleared != OLD.cntrbCleared) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbCleared',
           IFNULL(OLD.cntrbCleared, 'null'), IFNULL(NEW.cntrbCleared, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.cntrbActive != OLD.cntrbActive) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbActive',
           IFNULL(OLD.cntrbActive, 'null'), IFNULL(NEW.cntrbActive, 'null'), NOW(), 'UPDATE');
    END IF;

    /* cntrbUpdated is not necessary since it is a TimeStamp value. */

    IF (NEW.cntrbAdded != OLD.cntrbAdded) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('contribution', NEW.cntrbKey, 'cntrbAdded',
           IFNULL(DATE_FORMAT(OLD.cntrbAdded, '%Y-%m-%d %H:%i:%s.%f'), 'null'),
           IFNULL(DATE_FORMAT(NEW.cntrbAdded, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'UPDATE');
    END IF;

END$$

DROP TRIGGER IF EXISTS `insert_contribution`$$

CREATE TRIGGER `insert_contribution` AFTER INSERT on `contribution`
FOR EACH ROW
BEGIN

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
      ('contribution', NEW.cntrbKey, 'cntrbKey',
       'null', IFNULL(CAST(NEW.cntrbKey AS CHAR), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
      ('contribution', NEW.cntrbKey, 'cntrbDate',
       'null', DATE_FORMAT(NEW.cntrbDate, '%Y-%m-%d %H:%i:%s.%f'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
      ('contribution', NEW.cntrbKey, 'cntrbBatch',
       'null', IFNULL(CAST(NEW.cntrbBatch AS CHAR), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbSeq',
       'null', IFNULL(CAST(NEW.cntrbSeq AS CHAR), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbPplKey',
       'null', IFNULL(CAST(NEW.cntrbPplKey AS CHAR), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
		  ('contribution', NEW.cntrbKey, 'cntrbGeneral',
       'null', IFNULL(CAST(NEW.cntrbGeneral AS CHAR), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbBuilding',
       'null', IFNULL(CAST(NEW.cntrbBuilding AS CHAR), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbMissions',
       'null', IFNULL(CAST(NEW.cntrbMissions AS CHAR), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbDesignated',
       'null', IFNULL(CAST(NEW.cntrbDesignated AS CHAR), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbType',
       'null', IFNULL(NEW.cntrbType, 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
      ('contribution', NEW.cntrbKey, 'cntrbComments',
       'null', IFNULL(NEW.cntrbComments, 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbCleared',
       'null', IFNULL(NEW.cntrbCleared, 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
      (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbActive',
       'null', IFNULL(NEW.cntrbActive, 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
 	  VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbUpdated',
       'null', IFNULL(DATE_FORMAT(NEW.cntrbUpdated, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

    INSERT INTO `ChurchLogging`
		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
 	  VALUES
 		  ('contribution', NEW.cntrbKey, 'cntrbAdded',
       'null', IFNULL(DATE_FORMAT(NEW.cntrbAdded, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

END$$

/* ******************************************************* */
/*                                                         */
/* Create the triggers for the people table. On UPDATE     */
/* save the old and new values IF they have been changed.  */
/* On INSERT log all the NEW values.                       */
/*                                                         */
/* ******************************************************* */

DROP TRIGGER IF EXISTS `update_people`$$

CREATE TRIGGER `update_people` AFTER UPDATE on `people`
FOR EACH ROW
BEGIN

    /* pplKey is not checked because it is the primary key, autoseq. */

    IF (NEW.pplTitle != OLD.pplTitle) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplTitle',
           IFNULL(OLD.pplTitle, 'null'), IFNULL(NEW.pplTitle, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplFirstName != OLD.pplFirstName) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFirstName',
           IFNULL(OLD.pplFirstName, 'null'), IFNULL(NEW.pplFirstName, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplLastName != OLD.pplLastName) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplLastName',
           IFNULL(OLD.pplLastName, 'null'), IFNULL(NEW.pplLastName, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplAddr1 != OLD.pplAddr1) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAddr1',
           IFNULL(OLD.pplAddr1, 'null'), IFNULL(NEW.pplAddr1, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplAddr2 != OLD.pplAddr2) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAddr2',
           IFNULL(OLD.pplAddr2, 'null'), IFNULL(NEW.pplAddr2, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplCity != OLD.pplCity) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCity',
           IFNULL(OLD.pplCity, 'null'), IFNULL(NEW.pplCity, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplState != OLD.pplState) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplState',
           IFNULL(OLD.pplState, 'null'), IFNULL(NEW.pplState, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplZip != OLD.pplZip) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplZip',
           IFNULL(OLD.pplZip, 'null'), IFNULL(NEW.pplZip, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplPhone1 != OLD.pplPhone1) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplPhone1',
           IFNULL(OLD.pplPhone1, 'null'), IFNULL(NEW.pplPhone1, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplPhone2 != OLD.pplPhone2) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplPhone2',
           IFNULL(OLD.pplPhone2, 'null'), IFNULL(NEW.pplPhone2, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplFaxNum != OLD.pplFaxNum) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFaxNum',
           IFNULL(OLD.pplFaxNum, 'null'), IFNULL(NEW.pplFaxNum, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplCellPhone != OLD.pplCellPhone) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCellPhone',
           IFNULL(OLD.pplCellPhone, 'null'), IFNULL(NEW.pplCellPhone, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplEMail != OLD.pplEMail) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEMail',
           IFNULL(OLD.pplEMail, 'null'), IFNULL(NEW.pplEMail, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplCustom != OLD.pplCustom) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCustom',
           IFNULL(OLD.pplCustom, 'null'), IFNULL(NEW.pplCustom, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplEnvelope != OLD.pplEnvelope) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEnvelope',
           IFNULL(OLD.pplEnvelope, 'null'), IFNULL(NEW.pplEnvelope, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplMailList != OLD.pplMailList) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplMailList',
           IFNULL(OLD.pplMailList, 'null'), IFNULL(NEW.pplMailList, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplEMailList != OLD.pplEMailList) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEMailList',
           IFNULL(OLD.pplEMailList, 'null'), IFNULL(NEW.pplEMailList, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplActive != OLD.pplActive) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplActive',
           IFNULL(OLD.pplActive, 'null'), IFNULL(NEW.pplActive, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplTitleB != OLD.pplTitleB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplTitleB',
           IFNULL(OLD.pplTitleB, 'null'), IFNULL(NEW.pplTitleB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplFirstNameB != OLD.pplFirstNameB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFirstNameB',
           IFNULL(OLD.pplFirstNameB, 'null'), IFNULL(NEW.pplFirstNameB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplLastNameB != OLD.pplLastNameB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplLastNameB',
           IFNULL(OLD.pplLastNameB, 'null'), IFNULL(NEW.pplLastNameB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplAddr1B != OLD.pplAddr1B) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAddr1B',
           IFNULL(OLD.pplAddr1B, 'null'), IFNULL(NEW.pplAddr1B, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplAddr2B != OLD.pplAddr2B) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAddr2B',
           IFNULL(OLD.pplAddr2B, 'null'), IFNULL(NEW.pplAddr2B, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplCityB != OLD.pplCityB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCityB',
           IFNULL(OLD.pplCityB, 'null'), IFNULL(NEW.pplCityB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplStateB != OLD.pplStateB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplStateB',
           IFNULL(OLD.pplStateB, 'null'), IFNULL(NEW.pplStateB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplZipB != OLD.pplZipB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplZipB',
           IFNULL(OLD.pplZipB, 'null'), IFNULL(NEW.pplZipB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplPhone1B != OLD.pplPhone1B) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplPhone1B',
           IFNULL(OLD.pplPhone1B, 'null'), IFNULL(NEW.pplPhone1B, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplPhone2B != OLD.pplPhone2B) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplPhone2B',
           IFNULL(OLD.pplPhone2B, 'null'), IFNULL(NEW.pplPhone2B, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplFaxNumB != OLD.pplFaxNumB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFaxNumB',
           IFNULL(OLD.pplFaxNumB, 'null'), IFNULL(NEW.pplFaxNumB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplCellPhoneB != OLD.pplCellPhoneB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCellPhoneB',
           IFNULL(OLD.pplCellPhoneB, 'null'), IFNULL(NEW.pplCellPhoneB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplEMailB != OLD.pplEMailB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEMailB',
           IFNULL(OLD.pplEMailB, 'null'), IFNULL(NEW.pplEMailB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplCustomB != OLD.pplCustomB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCustomB',
           IFNULL(OLD.pplCustomB, 'null'), IFNULL(NEW.pplCustomB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplMailListB != OLD.pplMailListB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplMailListB',
           IFNULL(OLD.pplMailListB, 'null'), IFNULL(NEW.pplMailListB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplEMailListB != OLD.pplEMailListB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEMailListB',
           IFNULL(OLD.pplEMailListB, 'null'), IFNULL(NEW.pplEMailListB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplActiveB != OLD.pplActiveB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplActiveB',
           IFNULL(OLD.pplActiveB, 'null'), IFNULL(NEW.pplActiveB, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplFromB != OLD.pplFromB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFromB',
           IFNULL(DATE_FORMAT(OLD.pplFromB, '%Y-%m-%d %H:%i:%s.%f'), 'null'),
           IFNULL(DATE_FORMAT(NEW.pplFromB, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplToB != OLD.pplToB) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplToB',
           IFNULL(DATE_FORMAT(OLD.pplToB, '%Y-%m-%d %H:%i:%s.%f'), 'null'),
           IFNULL(DATE_FORMAT(NEW.pplToB, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplUpdated != OLD.pplUpdated) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplUpdated',
           IFNULL(DATE_FORMAT(OLD.pplUpdated, '%Y-%m-%d %H:%i:%s.%f'), 'null'),
           IFNULL(DATE_FORMAT(NEW.pplUpdated, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplAdded != OLD.pplAdded) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAdded',
           IFNULL(DATE_FORMAT(OLD.pplAdded, '%Y-%m-%d %H:%i:%s.%f'), 'null'),
           IFNULL(DATE_FORMAT(NEW.pplAdded, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.pplGivingByEmail != OLD.pplGivingByEmail) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplGivingByEmail',
           IFNULL(OLD.pplGivingByEmail, 'null'), IFNULL(NEW.pplGivingByEmail, 'null'), NOW(), 'UPDATE');
    END IF;

END$$

DROP TRIGGER IF EXISTS `insert_people`$$

CREATE TRIGGER `insert_people` AFTER INSERT on `people`
FOR EACH ROW
BEGIN

        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
          ('people', NEW.pplKey, 'pplKey', 'null', IFNULL(CAST(NEW.pplKey AS CHAR), 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplTitle', 'null', IFNULL(NEW.pplTitle, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFirstName', 'null', IFNULL(NEW.pplFirstName, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplLastName', 'null', IFNULL(NEW.pplLastName, 'null'), NOW(), 'INDERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAddr1', 'null', IFNULL(NEW.pplAddr1, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAddr2', 'null', IFNULL(NEW.pplAddr2, 'null'), NOW(), 'INDERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCity', 'null', IFNULL(NEW.pplCity, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplState', 'null', IFNULL(NEW.pplState, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplZip', 'null', IFNULL(NEW.pplZip, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplPhone1', 'null', IFNULL(NEW.pplPhone1, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplPhone2', 'null', IFNULL(NEW.pplPhone2, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFaxNum', 'null', IFNULL(NEW.pplFaxNum, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCellPhone', 'null', IFNULL(NEW.pplCellPhone, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEMail', 'null', IFNULL(NEW.pplEMail, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCustom', 'null', IFNULL(NEW.pplCustom, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEnvelope', 'null', IFNULL(NEW.pplEnvelope, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplMailList', 'null', IFNULL(NEW.pplMailList, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEMailList', 'null', IFNULL(NEW.pplEMailList, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplActive', 'null', IFNULL(NEW.pplActive, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplTitleB', 'null', IFNULL(NEW.pplTitleB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFirstNameB', 'null', IFNULL(NEW.pplFirstNameB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplLastNameB', 'null', IFNULL(NEW.pplLastNameB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAddr1B', 'null', IFNULL(NEW.pplAddr1B, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAddr2B', 'null', IFNULL(NEW.pplAddr2B, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCityB', 'null', IFNULL(NEW.pplCityB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplStateB', 'null', IFNULL(NEW.pplStateB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplZipB', 'null', IFNULL(NEW.pplZipB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplPhone1B', 'null', IFNULL(NEW.pplPhone1B, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplPhone2B', 'null', IFNULL(NEW.pplPhone2B, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFaxNumB', 'null', IFNULL(NEW.pplFaxNumB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCellPhoneB', 'null', IFNULL(NEW.pplCellPhoneB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEMailB', 'null', IFNULL(NEW.pplEMailB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplCustomB', 'null', IFNULL(NEW.pplCustomB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplMailListB', 'null', IFNULL(NEW.pplMailListB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplEMailListB', 'null', IFNULL(NEW.pplEMailListB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplActiveB', 'null', IFNULL(NEW.pplActiveB, 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplFromB', 'null',
           IFNULL(DATE_FORMAT(NEW.pplFromB, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplToB', 'null',
           IFNULL(DATE_FORMAT(NEW.pplToB, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplUpdated', 'null',
           IFNULL(DATE_FORMAT(NEW.pplUpdated, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplAdded', 'null',
           IFNULL(DATE_FORMAT(NEW.pplAdded, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('people', NEW.pplKey, 'pplGivingByEmail', 'null', IFNULL(NEW.pplGivingByEmail, 'null'), NOW(), 'INSERT');

END$$

/* ******************************************************* */
/*                                                         */
/* Create the triggers for the parms table. On UPDATE save */
/* the old and new values IF they have been changed. On    */
/* INSERT log all the NEW values.                          */
/*                                                         */
/* ******************************************************* */

DROP TRIGGER IF EXISTS `update_parms`$$

CREATE TRIGGER `update_parms` AFTER UPDATE on `parms`
FOR EACH ROW
BEGIN

    /* prmsKey is not checked because it is the primary key, autoseq. */

    IF (NEW.prmsSrc != OLD.prmsSrc) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('parms', NEW.prmsKey, 'prmsSrc',
           IFNULL(OLD.prmsSrc, 'null'), IFNULL(NEW.prmsSrc, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.prmsValue != OLD.prmsValue) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('parms', NEW.prmsKey, 'prmsValue',
           IFNULL(OLD.prmsValue, 'null'), IFNULL(NEW.prmsValue, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.prmsActive != OLD.prmsActive) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('parms', NEW.prmsKey, 'prmsActive',
           IFNULL(OLD.prmsActive, 'null'), IFNULL(NEW.prmsActive, 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.prmsUpdated != OLD.prmsUpdated) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('parms', NEW.prmsKey, 'prmsUpdated',
           IFNULL(DATE_FORMAT(OLD.prmsUpdated, '%Y-%m-%d %H:%i:%s.%f'), 'null'),
           IFNULL(DATE_FORMAT(NEW.prmsUpdated, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'UPDATE');
    END IF;

END$$

DROP TRIGGER IF EXISTS `insert_parms`$$

CREATE TRIGGER `insert_parms` AFTER INSERT on `parms`
FOR EACH ROW
BEGIN

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('parms', NEW.prmsKey, 'prmsKey', 'null', IFNULL(CAST(NEW.prmsKey AS CHAR), 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('parms', NEW.prmsKey, 'prmsSrc', 'null', IFNULL(NEW.prmsSrc, 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('parms', NEW.prmsKey, 'prmsValue', 'null', IFNULL(NEW.prmsValue, 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('parms', NEW.prmsKey, 'prmsActive', 'null', IFNULL(NEW.prmsActive, 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('parms', NEW.prmsKey, 'prmsUpdated', 'null',
     IFNULL(DATE_FORMAT(NEW.prmsUpdated, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

END$$

/* ******************************************************* */
/*                                                         */
/* Create the triggers for the envelopes table. On UPDATE  */
/* save the old and new values IF they have been changed.  */
/* On INSERT log all the NEW values.                       */
/*                                                         */
/* ******************************************************* */

DROP TRIGGER IF EXISTS `update_envelopes`$$

CREATE TRIGGER `update_envelopes` AFTER UPDATE on `envelopes`
FOR EACH ROW
BEGIN

    /* Key is not checked because it is the primary key, autoseq. */

      IF (NEW.EnvelopeNo != OLD.EnvelopeNo) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('envelopes', NEW.`Key`, 'EnvelopeNo',
           IFNULL(CAST(OLD.EnvelopeNo AS CHAR), 'null'),
           IFNULL(CAST(NEW.EnvelopeNo AS CHAR), 'null'), NOW(), 'UPDATE');
      END IF;

      IF (NEW.PeopleNo != OLD.PeopleNo) THEN
        INSERT INTO `ChurchLogging`
          (log_table, table_id, `field`, old_value, new_value , modified, `action`)
        VALUES
    		  ('envelopes', NEW.`Key`, 'PeopleNo',
           IFNULL(CAST(OLD.PeopleNo AS CHAR), 'null'),
           IFNULL(CAST(NEW.PeopleNo AS CHAR), 'null'), NOW(), 'UPDATE');
      END IF;

    IF (NEW.DateStart != OLD.DateStart) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('envelopes', NEW.`Key`, 'DateStart',
           IFNULL(DATE_FORMAT(OLD.DateStart, '%Y-%m-%d %H:%i:%s.%f'), 'null'),
           IFNULL(DATE_FORMAT(NEW.DateStart, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.DateEnd != OLD.DateEnd) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('envelopes', NEW.`Key`, 'DateEnd',
           IFNULL(DATE_FORMAT(OLD.DateEnd, '%Y-%m-%d %H:%i:%s.%f'), 'null'),
           IFNULL(DATE_FORMAT(NEW.DateEnd, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'UPDATE');
    END IF;

    IF (NEW.Active != OLD.Active) THEN
        INSERT INTO `ChurchLogging`
    		  (log_table, table_id, `field`, old_value, new_value , modified, `action`)
    	  VALUES
    		  ('envelopes', NEW.`Key`, 'Active',
           IFNULL(OLD.Active, 'null'), IFNULL(NEW.Active, 'null'), NOW(), 'UPDATE');
    END IF;

END$$

DROP TRIGGER IF EXISTS `insert_envelopes`$$

CREATE TRIGGER `insert_envelopes` AFTER INSERT on `envelopes`
FOR EACH ROW
BEGIN

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('envelopes', NEW.`Key`, 'Key', 'null', IFNULL(CAST(NEW.`Key` AS CHAR), 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('envelopes', NEW.`Key`, 'EnvelopeNo', 'null',
     IFNULL(CAST(NEW.EnvelopeNo AS CHAR), 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('envelopes', NEW.`Key`, 'PeopleNo', 'null',
     IFNULL(CAST(NEW.PeopleNo AS CHAR), 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('envelopes', NEW.`Key`, 'DateStart', 'null',
     IFNULL(DATE_FORMAT(NEW.DateStart, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('envelopes', NEW.`Key`, 'DateEnd', 'null',
     IFNULL(DATE_FORMAT(NEW.DateEnd, '%Y-%m-%d %H:%i:%s.%f'), 'null'), NOW(), 'INSERT');

  INSERT INTO `ChurchLogging`
    (log_table, table_id, `field`, old_value, new_value , modified, `action`)
  VALUES
    ('envelopes', NEW.`Key`, 'Active', 'null', IFNULL(NEW.Active, 'null'), NOW(), 'INSERT');

END$$

DELIMITER ;

DELETE FROM parms WHERE prmsSrc = 'ReportVerifySubject';
INSERT INTO parms (prmsSrc, prmsValue)
  VALUE('ReportVerifySubject', 'Verification of Email address for First Baptist Giving Reports');

DELETE FROM parms WHERE prmsSrc = 'SMTPServer';
INSERT INTO parms (prmsSrc, prmsValue)
  VALUE('SMTPServer', 'www.aol.com');

DELETE FROM parms WHERE prmsSrc = 'MailUserID';
INSERT INTO parms (prmsSrc, prmsValue)
  VALUE('MailUserID', 'userid');

DELETE FROM parms WHERE prmsSrc = 'MailPassword';
INSERT INTO parms (prmsSrc, prmsValue)
  VALUE('MailPassword', 'password');

DELETE FROM parms WHERE prmsSrc = 'ContributorTemplates';
INSERT INTO parms (prmsSrc, prmsValue)
  VALUE('ContributorTemplates', 'VerifyEMail.html');

DELETE FROM parms WHERE prmsSrc = 'ReportTemplates';
INSERT INTO parms (prmsSrc, prmsValue)
  VALUE('ReportTemplates', 'C:\\EZSoftware\\EZTeller2\\Reports');
  