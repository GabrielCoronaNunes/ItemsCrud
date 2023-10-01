CREATE DATABASE crudItems;
USE crudItems;

CREATE TABLE items
(
    id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(255)
);