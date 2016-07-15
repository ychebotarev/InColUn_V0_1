CREATE SCHEMA `incolun`;

DROP TABLE IF EXISTS `incolun`.`users`;

CREATE TABLE IF NOT EXISTS `incolun`.`users` (
  `id` BIGINT UNSIGNED NOT NULL,
  `login_string` VARCHAR(255) NOT NULL,
  `password_hash` VARCHAR(50),
  `salt` INT UNSIGNED,  
  `display_name` VARCHAR(255) NOT NULL,  
  `email` VARCHAR(255),
  `auth_provider` ENUM('L', 'F', 'G') NOT NULL,
  `created` DATETIME NOT NULL DEFAULT NOW(),
  `status` ENUM('N', 'A', 'D') NOT NULL DEFAULT 'N',
  PRIMARY KEY (`id`, `login_string`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `login_string_UNIQUE` (`login_string` ASC)
  )
ENGINE = InnoDB;

SELECT * FROM incolun.users;
SELECT * FROM incolun.boards;

delete from incolun.users