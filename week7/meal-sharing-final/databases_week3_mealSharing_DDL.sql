CREATE DATABASE databases_week3_mealsharing DEFAULT CHARACTER SET = 'utf8mb4';
USE databases_week3_mealsharing;
CREATE TABLE `meal` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `title` VARCHAR(255) NOT NULL,
  `description` TEXT NOT NULL,
  `location` VARCHAR(255) NULL,
  `when` DATETIME NOT NULL,
  `max_reservations` INT(10) NOT NULL,
  `price` DECIMAL(7, 2) NOT NULL,
  `created_date` DATE NOT NULL
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;
CREATE TABLE `reservation` (
  `id` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `number_of_guests` INT(10) NOT NULL,
  `created_date` DATE NOT NULL,
  `contact_phonenumber` VARCHAR(255) NOT NULL,
  `contact_name` VARCHAR(255) NOT NULL,
  `contact_email` VARCHAR(255) NOT NULL,
  `meal_id` INT(10) UNSIGNED NOT NULL,
  CONSTRAINT `fk_reservation_meal` FOREIGN KEY (`meal_id`) REFERENCES `meal` (`id`) ON DELETE
  SET
    NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;
CREATE TABLE `review` (
  `id` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `title` VARCHAR(255) NOT NULL,
  `description` TEXT NOT NULL,
  `stars` INT (10) NOT NULL,
  `created_date` DATE NOT NULL,
  `meal_id` INT(10) UNSIGNED NOT NULL,
  CONSTRAINT `fk_review_meal` FOREIGN KEY (`meal_id`) REFERENCES `meal` (`id`) ON DELETE
  SET
    NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;
INSERT INTO
  meal(
    `title`,
    `description`,
    `location`,
    `when`,
    `max_reservations`,
    `price`,
    `created_date`
  )
VALUES(
    'dumplings',
    'very delicious dumpling with chicken and vegetables',
    'china town, lyngby',
    '2022-02-26 19:00:00',
    100,
    60,
    '2022-02-15'
  ),(
    'pizza',
    'improvised chinese pizza margarita',
    'china town, lyngby',
    '2022-02-25 20:15:00',
    200,
    70,
    '2022-02-16'
  ),(
    'rice',
    'traditional rice with chicken currey',
    'china town, lyngby',
    '2022-02-26 21:00:00',
    50,
    99,
    '2022-02-16'
  ),(
    'burger',
    ' grilled burger with cheeze and chicken nuggets',
    'china town, lyngby',
    '2022-02-25 18:15:00',
    150,
    60,
    '2022-02-15'
  ),(
    'pita bread',
    'home made pita bread',
    'china town, lyngby',
    '2022-02-24 15:30:00',
    100,
    40,
    '2022-02-14'
  ),(
    'sandwitch',
    'burger with choise of fill chcken,salat,tuna and a sodavand free',
    'china town, lyngby',
    '2022-02-25 14:00:00',
    80,
    35,
    '2022-02-16'
  );
INSERT INTO
  reservation(
    `number_of_guests`,
    `created_date`,
    `contact_phonenumber`,
    `contact_name`,
    `contact_email`,
    `meal_id`
  )
VALUES(
    80,
    '2022-02-16',
    '333333333',
    'asma',
    'asma@gmail.com',
    1
  ),(
    150,
    '2022-02-16',
    '34343434',
    'chunmei',
    'chunmei@gmail.com',
    2
  ),(
    50,
    '2022-02-16',
    '5467876',
    'amrit',
    'amrit@gmail.com',
    3
  ),(
    100,
    '2022-02-16',
    '989989898',
    'sweta',
    'sweta@gmail.com',
    4
  ),(
    70,
    '2022-02-16',
    '75757575',
    'anas',
    'anas@gmail.com',
    5
  ),(
    90,
    '2022-02-17',
    '333333333',
    'negar',
    'negar@gmail.com',
    1
  ),(
    80,
    '2022-02-16',
    '88888222',
    'maryan',
    'maryam@gmail.com',
    3
  ),(
    70,
    '2022-02-16',
    '34343434',
    'quais',
    'quais@gmail.com',
    4
  ),(
    60,
    '2022-02-16',
    '34343434',
    'vahab',
    'vahab@gmail.com',
    5
  ),(
    80,
    '2022-02-17',
    '34343434',
    'anas',
    'anas@gmail.com',
    6
  ),(
    200,
    '2022-02-16',
    '34343434',
    'chunmei',
    'chunmei@gmail.com',
    2
  );
INSERT INTO
  review(
    `title`,
    `description`,
    `stars`,
    `created_date`,
    `meal_id`
  )
VALUES(
    'good',
    'very good food in town',
    5,
    '2022-02-26',
    2
  ),(
    'better food ',
    'very good food but service is bad in town',
    3,
    '2022-02-27',
    2
  ),(
    'good',
    'very good food in town',
    4,
    '2022-02-28',
    1
  ),(
    'average',
    'bad service good food in town',
    3,
    '2022-02-27',
    2
  ),(
    'exeta delicious',
    'very very good home made pitabread',
    5,
    '2022-02-27',
    5
  ),(
    'delicious',
    'delicious buger',
    4,
    '2022-02-28',
    6
  );