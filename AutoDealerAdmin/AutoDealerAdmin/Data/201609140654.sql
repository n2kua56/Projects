CREATE DATABASE  IF NOT EXISTS `autodealer` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `autodealer`;
-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: autodealer
-- ------------------------------------------------------
-- Server version	5.5.5-10.1.13-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `category` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL COMMENT 'The name of the category. Will be displayed in various places in the web site',
  `Description` varchar(512) DEFAULT NULL,
  `Seq` int(11) DEFAULT '5' COMMENT 'Relative numbers for determining the sequence the items should be listed.',
  `PageDescription` text COMMENT 'Descriptive text to be displayed on the category web page',
  `Active` int(11) DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1 COMMENT='Categories of vehicles. Every Vehicle record must have an Id into this table to identify the category it will be listed under.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'convertible','Convertibles',2,NULL,1),(2,'luxury','Luxury',4,NULL,1),(3,'sedan','Sedans',5,NULL,1),(4,'trucks','Trucks',7,NULL,1),(5,'coupe','Coupe',3,'This is a message.',1),(6,'awd4wd','All Wheel Drive & 4 Wheel Drive',1,NULL,1),(7,'suvcrossover','SUV & Crossovers',6,NULL,1),(8,'TestCat','Test Category',5,'Test web page description for test category',1);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `gallary`
--

DROP TABLE IF EXISTS `gallary`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `gallary` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `VehicleId` int(11) NOT NULL COMMENT 'Id in the vehicle table that this gallary picture belongs to.',
  `GallaryPic` varchar(128) NOT NULL COMMENT 'The full file name (minus path) of this GallaryPic. The path will come from the "GallaryPicPath" property.',
  `LargePic` varchar(128) NOT NULL COMMENT 'Full file name (minus path) of the "Large" picture that will be displayed when a user clicks on a gallery picture. The path wil come fro m the LargeGallaryPath property',
  `Seq` int(11) DEFAULT '5' COMMENT 'The relative sequence number to determine the order the items will be displayed. Duplicate Seq numbers are allowed but items with the same Seq number can''t have the order assured.',
  `Upload` int(11) DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `gallary_vehicle_idx` (`VehicleId`),
  CONSTRAINT `gallary_vehicle` FOREIGN KEY (`VehicleId`) REFERENCES `vehicles` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1 COMMENT='Sets up Vehicle Gallaries. Each vericle will have 0 or more gallery photos.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gallary`
--

LOCK TABLES `gallary` WRITE;
/*!40000 ALTER TABLE `gallary` DISABLE KEYS */;
INSERT INTO `gallary` VALUES (1,1,'Bluetruck0G.jpg','BlueTruck0L.jpg',1,1),(2,1,'BlueTruck1G.jpg','BlueTruck1L.jpg',2,1),(3,1,'BlueTruck2G.jpg','BlueTruck2L.jpg',3,1),(4,1,'BlueTruck3G.jpg','BlueTruck3L.jpg',4,1),(5,1,'BlueTruck4G.jpg','BlueTruck4L.jpg',5,1),(6,1,'BlueTruck5G.jpg','BlueTruck5L.jpg',6,1),(7,1,'BlueTruck6G.jpg','BlueTruck6L.jpg',7,1),(9,5,'MazdaMiata0G.jpg','MazdaMiata0L.jpg',1,1),(10,5,'MazdaMiata1G.jpg','MazdaMiata1L.jpg',2,1),(11,5,'MazdaMiata2G.jpg','MazdaMiata2L.jpg',3,1),(12,5,'MazdaMiata3G.jpg','MazdaMiata3L.jpg',4,1),(13,5,'MazdaMiata4G.jpg','MazdaMiata4L.jpg',5,1),(14,5,'MazdaMiata5G.jpg','MazdaMiata5L.jpg',6,1),(15,5,'MazdaMiata6G.jpg','MazdaMiata6L.jpg',7,1),(16,6,'RedMustang0G.jpg','RedMustang0L.jpg',1,1),(17,7,'VWJetta0G.jpg','VWJetta0L.jpg',1,1),(18,7,'VWJetta1G.jpg','VWJetta1L.jpg',2,1),(19,7,'VWJetta2G.jpg','VWJetta2L.jpg',3,1),(20,7,'VWJetta3G.jpg','VWJetta3L.jpg',4,1),(21,7,'VWJetta4G.jpg','VWJetta4L.jpg',5,1),(22,7,'VWJetta5G.jpg','VWJetta5L.jpg',6,1),(23,7,'VWJetta6G.jpg','VWJetta6L.jpg',7,1),(24,8,'WhiteTruck0G.jpg','WhiteTruck0L.jpg',1,1);
/*!40000 ALTER TABLE `gallary` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `property`
--

DROP TABLE IF EXISTS `property`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `property` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL COMMENT 'Name of the property i.e. SmallPicPath',
  `Value` varchar(256) NOT NULL COMMENT 'The value assigned to the property name',
  `Description` varchar(512) DEFAULT NULL COMMENT 'Description of the property',
  `PageDescription` text,
  PRIMARY KEY (`Id`),
  KEY `propertyName` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1 COMMENT='Properties used in the website';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `property`
--

LOCK TABLES `property` WRITE;
/*!40000 ALTER TABLE `property` DISABLE KEYS */;
INSERT INTO `property` VALUES (1,'SmallPicPath','Vehicles/SmallPics','Relative path from images for small pictures.',NULL),(2,'LargePicPath','Vehicles/LargePics','Relative path from images for large pictures usually displayed when clicking a small picture',NULL),(3,'GallaryPicPath','Vehicles/GalleryPics','Relative path from images shown in a galery',NULL),(4,'LargeGallaryPath','Vehicles/LargeGalleryPics','Relative path from large images shown when clicking on a gallery pic',NULL),(5,'TestProp1','TestValue1','TestProp1 Description','TestProp1 Page Description'),(6,'testprop','TestValue','Description','Page Description'),(7,'LocalSmallPicPath','Staging/SmallPics','Relative path to staging area for small pics',NULL),(8,'LocalLargePicPath','Staging/LargePics','Relative path to staging area for large pics',NULL),(9,'LocalGallaryPicPath','Staging/GallaryPics','Relative path to staging area for Gallary pics',NULL);
/*!40000 ALTER TABLE `property` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicles`
--

DROP TABLE IF EXISTS `vehicles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vehicles` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(256) NOT NULL COMMENT 'Display name for the vehicle. i.e 2005 Black VW Jetta',
  `StartDate` datetime NOT NULL COMMENT 'Date to start displaying this vehicle in the web site.',
  `EndDate` datetime DEFAULT NULL COMMENT 'Date to remove the vehicle from the web site, null means don''t remove.',
  `SmallPic` varchar(512) NOT NULL COMMENT 'full file name (without path) of the "small" picture.  The path will come from the SmallPicPath property.',
  `LargePic` varchar(512) NOT NULL COMMENT 'Full file name (minus path) of the "Large" picture. The path name will come from the "LargePicPath" property.',
  `CategoryId` int(11) NOT NULL COMMENT 'Id in the category table that identifies the category this vehicle is placed under on the web site.',
  `Featured` int(11) NOT NULL DEFAULT '0' COMMENT 'Is this vehicle displayed on the "Inventory" page',
  `VIN` varchar(64) DEFAULT NULL COMMENT 'Vehicle Identification Number',
  `Price` double DEFAULT NULL COMMENT 'Sale Price',
  `PageDescription` text COMMENT 'Descriptive text that is displayed on the vehicle page',
  `ShortDescription` varchar(256) DEFAULT NULL COMMENT 'Text displayed next to the vehicle picture on the category page',
  `Active` int(11) DEFAULT '1',
  `SmallPicUpload` int(11) DEFAULT '1',
  `LargePicUpload` int(11) DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `vehicles_category_idx` (`CategoryId`),
  CONSTRAINT `vehicles_category` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1 COMMENT='The vehicles for the web site\n';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicles`
--

LOCK TABLES `vehicles` WRITE;
/*!40000 ALTER TABLE `vehicles` DISABLE KEYS */;
INSERT INTO `vehicles` VALUES (1,'Blue Truck','2016-08-24 00:00:00',NULL,'BlueTruck0S.jpg','BlueTruck0L.jpg',4,0,NULL,NULL,NULL,NULL,1,1,1),(5,'Mazda Miata','2016-08-24 00:00:00',NULL,'MazdaMiata0S.jpg','MazdaMiata0L.jpg',1,0,NULL,NULL,NULL,NULL,1,1,1),(6,'Red Mustang','2016-08-24 00:00:00',NULL,'RedMustang0S.jpg','RedMustang0L.jpg',1,0,NULL,NULL,NULL,NULL,1,1,1),(7,'VW Jetta','2016-08-24 00:00:00','2016-09-10 08:38:30','VWJetta0S.jpg','VWJetta0L.jpg',3,0,'1234',9999.89,'Longer Description','Short Description',1,1,1),(8,'White Truck','2016-08-24 00:00:00',NULL,'WhiteTruck0S.jpg','WhiteTruck0L.jpg',4,0,NULL,NULL,NULL,NULL,1,1,1),(9,'testName','2016-09-06 00:00:00','2016-10-06 00:00:00','BlueTruck4S.jpg','MazdaMiata3L.JPG',4,1,'12345',10000,'Page Desc modified','Short Desc modified',1,1,1);
/*!40000 ALTER TABLE `vehicles` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-09-14  6:54:21
