-- MySQL Script generated by MySQL Workbench
-- sáb 08 abr 2023 19:35:19
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema Inmobiliaria
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `Inmobiliaria` ;

-- -----------------------------------------------------
-- Schema Inmobiliaria
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Inmobiliaria` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_bin ;
USE `Inmobiliaria` ;

-- -----------------------------------------------------
-- Table `Inmobiliaria`.`Propietario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Inmobiliaria`.`Propietario` (
  `IdPropietario` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  `Apellido` VARCHAR(45) NULL,
  `Direccion` VARCHAR(45) NULL,
  `Telefono` VARCHAR(45) NULL,
  `Dni` VARCHAR(45) NULL,
  `Email` VARCHAR(45) NULL,
  PRIMARY KEY (`IdPropietario`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Inmobiliaria`.`Inmueble`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Inmobiliaria`.`Inmueble` (
  `IdInmueble` INT NOT NULL AUTO_INCREMENT,
  `Tipo` VARCHAR(10) NULL,
  `Coordenadas` VARCHAR(50) NULL,
  `Precio` DECIMAL NULL,
  `Ambientes` INT NULL,
  `Uso` VARCHAR(45) NULL,
  `Activo` TINYINT NULL,
  `IdPropietario` INT NOT NULL,
  PRIMARY KEY (`IdInmueble`),
  INDEX `fk_Inmueble_Propietario1_idx` (`IdPropietario` ASC) VISIBLE,
  CONSTRAINT `fk_Inmueble_Propietario1`
    FOREIGN KEY (`IdPropietario`)
    REFERENCES `Inmobiliaria`.`Propietario` (`IdPropietario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Inmobiliaria`.`Inquilino`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Inmobiliaria`.`Inquilino` (
  `IdInquilino` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  `Apellido` VARCHAR(45) NULL,
  `Correo` VARCHAR(45) NULL,
  `Dni` VARCHAR(45) NULL,
  `Telefono` VARCHAR(45) NULL,
  `FechaNacimiento` VARCHAR(45) NULL,
  PRIMARY KEY (`IdInquilino`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Inmobiliaria`.`Contrato`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Inmobiliaria`.`Contrato` (
  `IdContrato` INT NOT NULL AUTO_INCREMENT,
  `IdInquilino` INT NOT NULL,
  `IdInmueble` INT NOT NULL,
  `FechaInicio` DATETIME NULL,
  `FechaFin` DATETIME NULL,
  PRIMARY KEY (`IdContrato`),
  INDEX `fk_Contrato_Inquilino1_idx` (`IdInquilino` ASC) VISIBLE,
  INDEX `fk_Contrato_Inmueble1_idx` (`IdInmueble` ASC) VISIBLE,
  CONSTRAINT `fk_Contrato_Inquilino1`
    FOREIGN KEY (`IdInquilino`)
    REFERENCES `Inmobiliaria`.`Inquilino` (`IdInquilino`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Contrato_Inmueble1`
    FOREIGN KEY (`IdInmueble`)
    REFERENCES `Inmobiliaria`.`Inmueble` (`IdInmueble`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Inmobiliaria`.`Pago`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Inmobiliaria`.`Pago` (
  `IdPago` INT NOT NULL AUTO_INCREMENT,
  `Fecha` DATETIME NULL,
  `NumeroPago` INT NULL,
  `Importe` DECIMAL NULL,
  `IdContrato` INT NOT NULL,
  PRIMARY KEY (`IdPago`, `IdContrato`),
  INDEX `fk_Pago_Contrato1_idx` (`IdContrato` ASC) VISIBLE,
  CONSTRAINT `fk_Pago_Contrato1`
    FOREIGN KEY (`IdContrato`)
    REFERENCES `Inmobiliaria`.`Contrato` (`IdContrato`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
