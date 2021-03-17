-- --------------------------------------------------------
-- Хост:                         mysql16.hostland.ru
-- Версия сервера:               5.7.31-34-log - Percona Server (GPL), Release 34, Revision 2e68637
-- Операционная система:         Linux
-- HeidiSQL Версия:              11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Дамп структуры для таблица host1608830_fouls.carmodels
DROP TABLE IF EXISTS `carmodels`;
CREATE TABLE IF NOT EXISTS `carmodels` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `carname` varchar(50) NOT NULL DEFAULT '',
  `year` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `carname` (`carname`,`year`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 COMMENT='cars';

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица host1608830_fouls.fouls
DROP TABLE IF EXISTS `fouls`;
CREATE TABLE IF NOT EXISTS `fouls` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idregistration` int(11) NOT NULL DEFAULT '0',
  `datef` date NOT NULL,
  `forfeit` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_REG` (`idregistration`),
  KEY `FK_FORFIET` (`forfeit`),
  CONSTRAINT `FK_FORFIET` FOREIGN KEY (`forfeit`) REFERENCES `typefouls` (`id`),
  CONSTRAINT `FK_REG` FOREIGN KEY (`idregistration`) REFERENCES `registration` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица host1608830_fouls.owners
DROP TABLE IF EXISTS `owners`;
CREATE TABLE IF NOT EXISTS `owners` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица host1608830_fouls.registration
DROP TABLE IF EXISTS `registration`;
CREATE TABLE IF NOT EXISTS `registration` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idowner` int(11) NOT NULL DEFAULT '0',
  `idcarmodel` int(11) NOT NULL DEFAULT '0',
  `numid` char(6) NOT NULL DEFAULT '0' COMMENT 'carnumber',
  PRIMARY KEY (`id`),
  UNIQUE KEY `numid` (`numid`),
  KEY `FK_IDOWNER` (`idowner`),
  KEY `FK_IDCAR` (`idcarmodel`),
  CONSTRAINT `FK_IDCAR` FOREIGN KEY (`idcarmodel`) REFERENCES `carmodels` (`id`),
  CONSTRAINT `FK_IDOWNER` FOREIGN KEY (`idowner`) REFERENCES `owners` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица host1608830_fouls.typefouls
DROP TABLE IF EXISTS `typefouls`;
CREATE TABLE IF NOT EXISTS `typefouls` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `typename` varchar(255) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для представление host1608830_fouls.vfouls
DROP VIEW IF EXISTS `vfouls`;
-- Создание временной таблицы для обработки ошибок зависимостей представлений
CREATE TABLE `vfouls` (
	`id` INT(11) NOT NULL,
	`date` DATE NOT NULL,
	`idreg` INT(11) NULL,
	`numid` CHAR(6) NULL COMMENT 'carnumber' COLLATE 'utf8_general_ci',
	`name` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
	`idtypefoul` INT(11) NULL,
	`typename` VARCHAR(255) NULL COLLATE 'utf8_general_ci'
) ENGINE=MyISAM;

-- Дамп структуры для представление host1608830_fouls.vregistration
DROP VIEW IF EXISTS `vregistration`;
-- Создание временной таблицы для обработки ошибок зависимостей представлений
CREATE TABLE `vregistration` (
	`id` INT(11) NOT NULL,
	`number` CHAR(6) NOT NULL COMMENT 'carnumber' COLLATE 'utf8_general_ci',
	`carid` INT(11) NULL,
	`carname` VARCHAR(50) NULL COLLATE 'utf8_general_ci',
	`year` VARCHAR(10) NULL COLLATE 'utf8_general_ci',
	`ownerid` INT(11) NULL,
	`ownername` VARCHAR(255) NULL COLLATE 'utf8_general_ci'
) ENGINE=MyISAM;

-- Дамп структуры для представление host1608830_fouls.vfouls
DROP VIEW IF EXISTS `vfouls`;
-- Удаление временной таблицы и создание окончательной структуры представления
DROP TABLE IF EXISTS `vfouls`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vfouls` AS select `fouls`.`id` AS `id`,`fouls`.`datef` AS `date`,`reg`.`id` AS `idreg`,`reg`.`numid` AS `numid`,`owners`.`name` AS `name`,`tf`.`id` AS `idtypefoul`,`tf`.`typename` AS `typename` from (((`fouls` left join `registration` `reg` on((`fouls`.`idregistration` = `reg`.`id`))) left join `typefouls` `tf` on((`fouls`.`forfeit` = `tf`.`id`))) join `owners` on((`reg`.`idowner` = `owners`.`id`)));

-- Дамп структуры для представление host1608830_fouls.vregistration
DROP VIEW IF EXISTS `vregistration`;
-- Удаление временной таблицы и создание окончательной структуры представления
DROP TABLE IF EXISTS `vregistration`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `vregistration` AS select `reg`.`id` AS `id`,`reg`.`numid` AS `number`,`car`.`id` AS `carid`,`car`.`carname` AS `carname`,`car`.`year` AS `year`,`owners`.`id` AS `ownerid`,`owners`.`name` AS `ownername` from ((`registration` `reg` left join `carmodels` `car` on((`reg`.`idcarmodel` = `car`.`id`))) left join `owners` on((`reg`.`idowner` = `owners`.`id`)));

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
