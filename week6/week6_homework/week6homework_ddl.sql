CREATE DATABASE week6homework;
 use week6homework;

 
 CREATE TABLE
    IF NOT EXISTS `user` (
        `id` varchar(32) NOT NULL ,
        `name` varchar(100) NOT NULL DEFAULT '',
        `email` varchar(100) NOT NULL DEFAULT '',
		`address` varchar(100) NOT NULL DEFAULT '',
        PRIMARY KEY (`id`)
    ) ENGINE = InnoDB AUTO_INCREMENT = 3 DEFAULT CHARSET = utf8mb3;
    INSERT INTO
    `user` (`id`, `name`, `email`,`address`)
VALUES ('0000000000000000', 'sumab', 'sumanghimire79@yahoo.com', 'ishoj,denmark'), ('00000008989898989', 'aayusma', 'aayusma@gmail.com','ishoj, denmark');

select * from product;
 CREATE TABLE
    IF NOT EXISTS `product` (
        `id` varchar(32) NOT NULL,
        `name` varchar(100) NOT NULL DEFAULT '',
        `price` decimal(18, 2) NOT NULL DEFAULT '0.00',
          `description`  VARCHAR(1000),
        PRIMARY KEY (`id`)
    ) ENGINE = InnoDB AUTO_INCREMENT = 3 DEFAULT CHARSET = utf8mb3;
    INSERT INTO
    `product` (`id`, `name`, `price`,`description`)
VALUES ('0000000000000001', 'Car', 700000.00,' mazda3'), ('0000000000000002', 'Bike', 5000.00,'mountaian bike');
