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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.author_info: ~1 rows (approximately)
DELETE FROM `author_info`;
/*!40000 ALTER TABLE `author_info` DISABLE KEYS */;
INSERT INTO `author_info` (`id`, `surname`, `name`, `patronymic`, `born`) VALUES
	(1, 'Пушкин', 'Александр', 'Сергеевич', '1799');
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.books: ~1 rows (approximately)
DELETE FROM `books`;
/*!40000 ALTER TABLE `books` DISABLE KEYS */;
INSERT INTO `books` (`id`, `autthor_id`, `name`, `year`, `available`) VALUES
	(1, 1, 'Капитанская дочка', '1836', 1);
/*!40000 ALTER TABLE `books` ENABLE KEYS */;

-- Dumping structure for table biblioteka.borrowed_books
CREATE TABLE IF NOT EXISTS `borrowed_books` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login_id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL,
  `date` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_borrowed_books_user` (`login_id`),
  KEY `FK_borrowed_books_books` (`book_id`),
  CONSTRAINT `FK_borrowed_books_books` FOREIGN KEY (`book_id`) REFERENCES `books` (`id`),
  CONSTRAINT `FK_borrowed_books_user` FOREIGN KEY (`login_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.borrowed_books: ~0 rows (approximately)
DELETE FROM `borrowed_books`;
/*!40000 ALTER TABLE `borrowed_books` DISABLE KEYS */;
INSERT INTO `borrowed_books` (`id`, `login_id`, `book_id`, `date`) VALUES
	(1, 1, 1, '2021-03-04');
/*!40000 ALTER TABLE `borrowed_books` ENABLE KEYS */;

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

-- Dumping structure for table biblioteka.user
CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `lvl` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login` (`login`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.user: ~3 rows (approximately)
DELETE FROM `user`;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`id`, `login`, `password`, `lvl`) VALUES
	(1, 'A1caida', 'ch3bur', 3),
	(2, 'Kveda', '123', 1),
	(6, 'nya', '321', 1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- Dumping structure for table biblioteka.user_info
CREATE TABLE IF NOT EXISTS `user_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login_user` varchar(50) DEFAULT NULL,
  `surname` varchar(50) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `patronymic` varchar(50) DEFAULT NULL,
  `phone` varchar(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_user_info_user` (`login_user`),
  CONSTRAINT `FK_user_info_user` FOREIGN KEY (`login_user`) REFERENCES `user` (`login`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table biblioteka.user_info: ~3 rows (approximately)
DELETE FROM `user_info`;
/*!40000 ALTER TABLE `user_info` DISABLE KEYS */;
INSERT INTO `user_info` (`id`, `login_user`, `surname`, `name`, `patronymic`, `phone`) VALUES
	(2, 'A1caida', 'Ахмедханов', 'Рамис', 'Нурутдинович', '7934672398'),
	(3, 'Kveda', 'Микеев', 'Айрат', 'Дамирович', '7935462398'),
	(4, 'nya', 'Моисеев', 'Александр', 'Витальевич', '79345632365');
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

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               10.4.14-MariaDB - mariadb.org binary distribution
-- Операционная система:         Win64
-- HeidiSQL Версия:              11.2.0.6213
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Экспортируемые данные не выделены.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

