CREATE DATABASE IF NOT EXISTS `cinediario` CHARACTER SET utf8mb4;
USE `cinediario`;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

CREATE TABLE `Filmes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Titulo` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Diretor` varchar(80) CHARACTER SET utf8mb4 NOT NULL,
    `Genero` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Ano` int NOT NULL,
    `DuracaoMin` int NOT NULL,
    `Nota` int NULL,
    `Assistido` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Filmes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Usuarios` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Login` varchar(40) CHARACTER SET utf8mb4 NOT NULL,
    `SenhaHash` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Salt` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CriadoEm` datetime(6) NOT NULL,
    CONSTRAINT `PK_Usuarios` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `IX_Usuarios_Login` ON `Usuarios` (`Login`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261028120000_InitialCreate', '8.0.2');

COMMIT;
