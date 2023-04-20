-- MySQL dump 10.13  Distrib 8.0.32, for Linux (x86_64)
--
-- Host: 127.0.0.1    Database: Inmobiliaria
-- ------------------------------------------------------
-- Server version	8.0.32-0ubuntu0.22.04.2

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Contrato`
--

DROP TABLE IF EXISTS `Contrato`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Contrato` (
  `IdContrato` int NOT NULL AUTO_INCREMENT,
  `IdInquilino` int NOT NULL,
  `IdInmueble` int NOT NULL,
  `FechaInicio` datetime DEFAULT NULL,
  `FechaFin` datetime DEFAULT NULL,
  PRIMARY KEY (`IdContrato`),
  KEY `fk_Contrato_Inquilino1_idx` (`IdInquilino`),
  KEY `fk_Contrato_Inmueble1_idx` (`IdInmueble`),
  CONSTRAINT `fk_Contrato_Inmueble1` FOREIGN KEY (`IdInmueble`) REFERENCES `Inmueble` (`IdInmueble`),
  CONSTRAINT `fk_Contrato_Inquilino1` FOREIGN KEY (`IdInquilino`) REFERENCES `Inquilino` (`IdInquilino`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Contrato`
--

LOCK TABLES `Contrato` WRITE;
/*!40000 ALTER TABLE `Contrato` DISABLE KEYS */;
INSERT INTO `Contrato` VALUES (3,2,4,'2023-04-01 00:00:00','2023-04-30 00:00:00'),(4,2,5,'2023-05-01 00:00:00','2023-06-30 00:00:00'),(5,3,6,'2023-07-01 00:00:00','2023-08-31 00:00:00'),(6,4,7,'2023-09-01 00:00:00','2023-10-31 00:00:00'),(7,5,8,'2023-11-01 00:00:00','2023-12-31 00:00:00');
/*!40000 ALTER TABLE `Contrato` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Inmueble`
--

DROP TABLE IF EXISTS `Inmueble`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Inmueble` (
  `IdInmueble` int NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(20) COLLATE utf8mb4_bin DEFAULT NULL,
  `Coordenadas` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `Precio` decimal(10,0) DEFAULT NULL,
  `Ambientes` int DEFAULT NULL,
  `Uso` enum('residencial','comercial') COLLATE utf8mb4_bin DEFAULT NULL,
  `Activo` tinyint DEFAULT NULL,
  `IdPropietario` int NOT NULL,
  PRIMARY KEY (`IdInmueble`),
  KEY `fk_Inmueble_Propietario1_idx` (`IdPropietario`),
  CONSTRAINT `fk_Inmueble_Propietario1` FOREIGN KEY (`IdPropietario`) REFERENCES `Propietario` (`IdPropietario`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Inmueble`
--

LOCK TABLES `Inmueble` WRITE;
/*!40000 ALTER TABLE `Inmueble` DISABLE KEYS */;
INSERT INTO `Inmueble` VALUES (4,'departamento','10.4567,-65.4321',200000,3,'residencial',1,5),(5,'apartamento','12.5678,-67.9012',100000,2,'residencial',1,6),(6,'casa','10.4567,-65.4321',200000,3,'residencial',1,4),(7,'apartamento','12.5678,-67.9012',100000,2,'residencial',1,5),(8,'local comercial','13.2468,-68.1357',800000,2,'comercial',1,4),(11,'casa','54.8442,-63.0834',50000,2,'comercial',1,4),(12,'casa','54.8442,-63.0834',150000,4,'residencial',1,4);
/*!40000 ALTER TABLE `Inmueble` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Inquilino`
--

DROP TABLE IF EXISTS `Inquilino`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Inquilino` (
  `IdInquilino` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `Apellido` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `Email` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `Dni` varchar(10) COLLATE utf8mb4_bin DEFAULT NULL,
  `Telefono` varchar(13) COLLATE utf8mb4_bin DEFAULT NULL,
  `FechaNacimiento` datetime DEFAULT NULL,
  PRIMARY KEY (`IdInquilino`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Inquilino`
--

LOCK TABLES `Inquilino` WRITE;
/*!40000 ALTER TABLE `Inquilino` DISABLE KEYS */;
INSERT INTO `Inquilino` VALUES (1,'Juan','Pérez','juanperez@mail.com','12345678A','654321987','1990-03-21 05:02:00'),(2,'María','González','mariagonzalez@mail.com','87654321B','789456123','1985-11-25 00:00:00'),(3,'Carlos','Ruiz','carlosruiz@mail.com','56789012C','123789456','1995-05-02 00:00:00'),(4,'Ana','Martínez','anamartinez@mail.com','34567890D','456123789','1988-07-17 00:00:00'),(5,'Pedro','López','pedrolopez@mail.com','90123456E','987654321','1992-09-29 00:00:00');
/*!40000 ALTER TABLE `Inquilino` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Pago`
--

DROP TABLE IF EXISTS `Pago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Pago` (
  `IdPago` int NOT NULL AUTO_INCREMENT,
  `Fecha` datetime DEFAULT NULL,
  `NumeroPago` int DEFAULT NULL,
  `Importe` decimal(10,0) DEFAULT NULL,
  `IdContrato` int NOT NULL,
  PRIMARY KEY (`IdPago`),
  KEY `fk_Pago_Contrato1_idx` (`IdContrato`),
  CONSTRAINT `fk_Pago_Contrato1` FOREIGN KEY (`IdContrato`) REFERENCES `Contrato` (`IdContrato`)
) ENGINE=InnoDB AUTO_INCREMENT=238 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Pago`
--

LOCK TABLES `Pago` WRITE;
/*!40000 ALTER TABLE `Pago` DISABLE KEYS */;
INSERT INTO `Pago` VALUES (3,'2022-03-01 00:00:00',3,60000,5),(5,'2022-02-01 00:00:00',2,80000,5),(46,'2023-04-12 13:43:00',234,30000,3),(47,'2023-04-12 13:59:00',14,30000,6),(236,'2023-04-12 10:30:00',222,10000,3);
/*!40000 ALTER TABLE `Pago` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Propietario`
--

DROP TABLE IF EXISTS `Propietario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Propietario` (
  `IdPropietario` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `Apellido` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `Direccion` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `Telefono` varchar(13) COLLATE utf8mb4_bin DEFAULT NULL,
  `Dni` varchar(10) COLLATE utf8mb4_bin DEFAULT NULL,
  `Email` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  PRIMARY KEY (`IdPropietario`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Propietario`
--

LOCK TABLES `Propietario` WRITE;
/*!40000 ALTER TABLE `Propietario` DISABLE KEYS */;
INSERT INTO `Propietario` VALUES (2,'Laura','García','Avenida Europa 245','654987126','12345678A','lauragarcia@mail.com'),(3,'Jorge','Martínez','Avenida Europa 15','789456123','87654321B','jorgemartinez@mail.com'),(4,'Ana','Fernández','Calle Real 8','456789123','56789012C','anafernandez@mail.com'),(5,'Carlos','López','Plaza Mayor 1','123456789','34567890D','carloslopez@mail.com'),(6,'María','Sánchez','Calle Libertad 3','987654321','90123456E','mariasanchez@mail.com');
/*!40000 ALTER TABLE `Propietario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Usuario`
--

DROP TABLE IF EXISTS `Usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Usuario` (
  `IdUsuario` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) COLLATE utf8mb4_bin DEFAULT NULL,
  `Apellido` varchar(45) COLLATE utf8mb4_bin DEFAULT NULL,
  `Avatar` varchar(45) COLLATE utf8mb4_bin DEFAULT NULL,
  `Clave` varchar(45) COLLATE utf8mb4_bin DEFAULT NULL,
  `Email` varchar(45) COLLATE utf8mb4_bin DEFAULT NULL,
  `Rol` int DEFAULT NULL,
  `Dni` varchar(45) COLLATE utf8mb4_bin DEFAULT NULL,
  `Telefono` varchar(45) COLLATE utf8mb4_bin DEFAULT NULL,
  PRIMARY KEY (`IdUsuario`),
  UNIQUE KEY `Dni_UNIQUE` (`Dni`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Usuario`
--

LOCK TABLES `Usuario` WRITE;
/*!40000 ALTER TABLE `Usuario` DISABLE KEYS */;
INSERT INTO `Usuario` VALUES (3,'Fernando Daniel','Gonzalez','/Uploads/avatar_3.jpeg','1Ml74PPgdjWOxqeb/SWbBGq5wDfyV/f25WupRDBxLa0=','kaiserkey2@gmail.com',1,'123456789','2657534231'),(4,'Jose','Vizcay','/Uploads/avatar_4.png','1Ml74PPgdjWOxqeb/SWbBGq5wDfyV/f25WupRDBxLa0=','josevizcay@gmail.com',2,'123456783','2657544323');
/*!40000 ALTER TABLE `Usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-04-19 21:59:18
