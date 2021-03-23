-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.13-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for biblioteka
CREATE DATABASE IF NOT EXISTS `biblioteka` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `biblioteka`;

-- Dumping structure for table biblioteka.author_info
CREATE TABLE IF NOT EXISTS `author_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `surname` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `patronymic` varchar(50) DEFAULT NULL,
  `born` varchar(4) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.author_info: ~5 rows (approximately)
DELETE FROM `author_info`;
/*!40000 ALTER TABLE `author_info` DISABLE KEYS */;
INSERT INTO `author_info` (`id`, `surname`, `name`, `patronymic`, `born`) VALUES
	(1, 'Пушкин', 'Александр', 'Сергеевич', '1799'),
	(2, 'Есенин', 'Сергей', 'Александрович', '1895'),
	(3, 'Толстой', 'Лев', 'Николаевич', '1828'),
	(4, 'Маяковский', 'Владимир', 'Владимирович', '1893'),
	(5, 'Лермонтов', 'Михаил', 'Юрьевич', '1814');
/*!40000 ALTER TABLE `author_info` ENABLE KEYS */;

-- Dumping structure for table biblioteka.books
CREATE TABLE IF NOT EXISTS `books` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `autthor_id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `year` varchar(4) NOT NULL,
  `available` int(1) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_books_author_info` (`autthor_id`),
  CONSTRAINT `FK_books_author_info` FOREIGN KEY (`autthor_id`) REFERENCES `author_info` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.books: ~0 rows (approximately)
DELETE FROM `books`;
/*!40000 ALTER TABLE `books` DISABLE KEYS */;
INSERT INTO `books` (`id`, `autthor_id`, `name`, `year`, `available`) VALUES
	(1, 1, 'Капитанская дочка', '1836', 1),
	(2, 2, 'Анна Снегина', '1925', 1),
	(3, 1, 'Пиковая дама', '1834', 1);
/*!40000 ALTER TABLE `books` ENABLE KEYS */;

-- Dumping structure for table biblioteka.borrowed_books
CREATE TABLE IF NOT EXISTS `borrowed_books` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login_id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL DEFAULT 0,
  `date` timestamp NULL DEFAULT NULL,
  `date_back` timestamp NULL DEFAULT NULL,
  `date_end` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_borrowed_books_books` (`book_id`),
  KEY `FK_borrowed_books_user_info` (`login_id`),
  CONSTRAINT `FK_borrowed_books_books` FOREIGN KEY (`book_id`) REFERENCES `books` (`id`),
  CONSTRAINT `FK_borrowed_books_user_info` FOREIGN KEY (`login_id`) REFERENCES `user_info` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.borrowed_books: ~4 rows (approximately)
DELETE FROM `borrowed_books`;
/*!40000 ALTER TABLE `borrowed_books` DISABLE KEYS */;
INSERT INTO `borrowed_books` (`id`, `login_id`, `book_id`, `date`, `date_back`, `date_end`) VALUES
	(2, 5, 1, '2021-03-21 11:29:01', '2021-03-21 19:31:50', '2021-04-04 11:29:01'),
	(3, 5, 3, '2021-03-21 19:10:08', '2021-03-21 19:54:13', '2021-04-04 19:10:08'),
	(4, 5, 1, '2021-03-21 19:41:36', '2021-03-21 20:03:51', '2021-04-04 19:41:36'),
	(5, 6, 2, '2021-03-23 16:41:08', '2021-03-23 16:41:35', '2021-04-06 16:41:08'),
	(6, 6, 3, '2021-03-23 20:54:54', '2021-03-23 20:55:08', '2021-04-06 20:54:54');
/*!40000 ALTER TABLE `borrowed_books` ENABLE KEYS */;

-- Dumping structure for table biblioteka.logss
CREATE TABLE IF NOT EXISTS `logss` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login_id` int(11) NOT NULL,
  `oper` varchar(50) NOT NULL DEFAULT '',
  `time_when` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_ban_hammer_user_info` (`login_id`),
  CONSTRAINT `FK_ban_hammer_user_info` FOREIGN KEY (`login_id`) REFERENCES `user_info` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.logss: ~0 rows (approximately)
DELETE FROM `logss`;
/*!40000 ALTER TABLE `logss` DISABLE KEYS */;
INSERT INTO `logss` (`id`, `login_id`, `oper`, `time_when`) VALUES
	(1, 14, 'INSERT', '2021-03-10 19:48:15'),
	(2, 9, 'BAN', '2021-03-10 19:48:16');
/*!40000 ALTER TABLE `logss` ENABLE KEYS */;

-- Dumping structure for table biblioteka.magazine
CREATE TABLE IF NOT EXISTS `magazine` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `date` date NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.magazine: ~0 rows (approximately)
DELETE FROM `magazine`;
/*!40000 ALTER TABLE `magazine` DISABLE KEYS */;
/*!40000 ALTER TABLE `magazine` ENABLE KEYS */;

-- Dumping structure for table biblioteka.operation
CREATE TABLE IF NOT EXISTS `operation` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.operation: ~2 rows (approximately)
DELETE FROM `operation`;
/*!40000 ALTER TABLE `operation` DISABLE KEYS */;
INSERT INTO `operation` (`id`, `name`) VALUES
	(1, 'Ксерокопия'),
	(2, 'Печать');
/*!40000 ALTER TABLE `operation` ENABLE KEYS */;

-- Dumping structure for table biblioteka.postavshik
CREATE TABLE IF NOT EXISTS `postavshik` (
  `id` int(11) NOT NULL,
  `comp` varchar(50) NOT NULL,
  `phone` varchar(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.postavshik: ~0 rows (approximately)
DELETE FROM `postavshik`;
/*!40000 ALTER TABLE `postavshik` DISABLE KEYS */;
/*!40000 ALTER TABLE `postavshik` ENABLE KEYS */;

-- Dumping structure for table biblioteka.user_info
CREATE TABLE IF NOT EXISTS `user_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `lvl` int(11) NOT NULL,
  `surname` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `patronymic` varchar(50) DEFAULT NULL,
  `phone` varchar(11) NOT NULL,
  `ban` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login` (`login`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.user_info: ~6 rows (approximately)
DELETE FROM `user_info`;
/*!40000 ALTER TABLE `user_info` DISABLE KEYS */;
INSERT INTO `user_info` (`id`, `login`, `password`, `lvl`, `surname`, `name`, `patronymic`, `phone`, `ban`) VALUES
	(5, 'A1caida', 'ch3bur', 3, 'Ахмедханов', 'Рамис', 'Нурутдинович', '79347542389', 0),
	(6, 'nyaa', 'qsc', 1, 'Моисеев', 'Александр', 'Витальевич', '79347659023', 0),
	(7, 'Liora', '123', 1, 'Микеев', 'Айрат', 'Дамирович', '79347672356', 0),
	(8, 'mud', '123', 1, 'Иванов', 'Иван', 'Иванович', '79456236489', 0),
	(9, 'as', 'asd', 1, 'Фамилия', 'Имя', 'Отчество', '12345678900', 1),
	(14, 'dsfdsfds', 'sdfsfs', 1, 'sdfdsf', 'sdfdsfds', NULL, '324324', 1);
/*!40000 ALTER TABLE `user_info` ENABLE KEYS */;

-- Dumping structure for table biblioteka.ychet
CREATE TABLE IF NOT EXISTS `ychet` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_oper` int(11) NOT NULL,
  `money` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_ychet_operation` (`id_oper`),
  CONSTRAINT `FK_ychet_operation` FOREIGN KEY (`id_oper`) REFERENCES `operation` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.ychet: ~0 rows (approximately)
DELETE FROM `ychet`;
/*!40000 ALTER TABLE `ychet` DISABLE KEYS */;
INSERT INTO `ychet` (`id`, `id_oper`, `money`) VALUES
	(1, 1, '10');
/*!40000 ALTER TABLE `ychet` ENABLE KEYS */;

-- Dumping structure for trigger biblioteka.ban_hammer
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ZERO_IN_DATE,NO_ZERO_DATE,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `ban_hammer` AFTER UPDATE ON `user_info` FOR EACH ROW BEGIN
if new.ban = 1 then
INSERT INTO biblioteka.logss SET logss.login_id = new.id, logss.oper = "BAN", logss.time_when = NOW();
ELSE
INSERT INTO biblioteka.logss SET logss.login_id = new.id, logss.oper = "UNBAN", logss.time_when = NOW();
END if;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- Dumping structure for trigger biblioteka.logger
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ZERO_IN_DATE,NO_ZERO_DATE,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `logger` AFTER INSERT ON `user_info` FOR EACH ROW BEGIN
INSERT INTO biblioteka.logss SET logss.login_id = NEW.id, logss.oper = "INSERT", logss.time_when = NOW();
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
