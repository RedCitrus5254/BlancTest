CREATE DATABASE homework;

CREATE TABLE IF NOT EXISTS todoItems (
   Id uuid UNIQUE NOT NULL,
   Title VARCHAR(255) NOT NULL,
   IsCompleted boolean NOT NULL,
   PRIMARY KEY (Id)
);