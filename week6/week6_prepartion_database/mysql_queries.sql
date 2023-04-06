/*
 CREATE DATABASE mealsharing_dotnetEx;
 use mealsharing_dotnetEx;
 CREATE TABLE user ( user varchar(255));
 alter table user add host varchar(255); */

-- Dumping database structure for dapper

CREATE DATABASE
    IF NOT EXISTS `dapper`
    /*!40100 DEFAULT CHARACTER SET utf8 */
    /*!80016 DEFAULT ENCRYPTION='N' */
;

USE `dapper`;

-- Dumping structure for table dapper.products

CREATE TABLE
    IF NOT EXISTS `products` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` varchar(100) NOT NULL DEFAULT '',
        `price` decimal(18, 2) NOT NULL DEFAULT '0.00',
        PRIMARY KEY (`id`)
    ) ENGINE = InnoDB AUTO_INCREMENT = 3 DEFAULT CHARSET = utf8mb3;

-- Dumping data for table dapper.products: ~0 rows (approximately)

/* !40000 ALTER TABLE `products` DISABLE KEYS */

INSERT INTO
    `products` (`id`, `name`, `price`)
VALUES (1, 'Car', 700000.00), (2, 'Bike', 5000.00);

/* to create new column which is not null*/

-- ALTER TABLE `products` add `description` VARCHAR(1000) NOT NULL DEFAULT '';

/* the column is altered to be null so that it can be posted without specifying column name*/

-- ALTER TABLE `products` modify COLUMN `description` VARCHAR(1000) NULL;

/* !40000 ALTER TABLE `products` ENABLE KEYS */

/* !40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */

/* !40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */

/* !40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */

/* !40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */

select * from products;
/* salesman*/
CREATE TABLE
    IF NOT EXISTS `salesman` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` varchar(100) NOT NULL DEFAULT '',
        `email` varchar(255) NOT NULL DEFAULT 'test@sales.dk',
        PRIMARY KEY (`id`)
    ) ENGINE = InnoDB AUTO_INCREMENT = 3 DEFAULT CHARSET = utf8mb3;
    INSERT INTO
    `salesman` (`id`, `name`, `email`)
VALUES (1, 'suman', 'suman@yahoo.com'), (2, 'aayusma', 'aayusma@gmail.com');
select * from salesman;

CREATE TABLE `sales` (
  `id` INT    NOT NULL AUTO_INCREMENT primary key,
  `shop_name` VARCHAR(255) NOT NULL default 'CPH Corner',
  `shop_address` VARCHAR(255) NOT NULL default 'youaskmehovedgade -58',
  `sales_date` DATE NOT NULL,
  `salesman_id` INT ,
  `products_id` INT ,
CONSTRAINT `fk_salesSalesman` FOREIGN KEY (`salesman_id`) REFERENCES `salesman` (`id`) ON DELETE   set null on  UPDATE CASCADE,
CONSTRAINT `fk_salesProducts` FOREIGN KEY (`products_id`) REFERENCES `products` (`id`) ON DELETE set null ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

 INSERT INTO
    `sales` (`id`, `shop_name`, `shop_address`, `sales_date`, `salesman_id`, `products_id`)
VALUES (1, 'gg','dd','2022-02-25 20:15:00', 1,1), (2, 'mm','mm','2022-02-25 20:15:00', 1,2);
select * from sales;