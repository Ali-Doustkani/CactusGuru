# Cactus Guru
I created this thing when I had some plants! (Actually it was more than some! About 800 species and varieties!)
You can manage your collection, add genera, taxa and print labels, and etc...

![Screenshot](https://gdurl.com/fIuF)

## Design
You may find this project over-engineered at first look. That's becuase I was trying to solve some of my issues with Enterprise Architecture, MVVM, DDD, and IoC. So I used this project not only for my plants, but also as a proof of concept to implement the things that I was learning at the time.
Here is technologies and patterns that used in this project:
* ORM, EF
* SQL Server Database Project for tracking db object changes with SCM
* WPF, MVVM with testable view-models
* Service Layer (as Facades for the Domain)
* DDD with testable domain entities
* IoC Container, Composition Root, StructureMap

## Getting Started
Before you get started you need to make sure you have .Net4.5 and MS Sql LocalDb installed on your computer. 
After that you have two options to run the app:
* Create a new database
* Use my sample database

### Creating the Database
In order to create a new database first build the entire solution, then right click on the CactusGuru.Persistance.Database and select the Publish.
Click the Edit button, and in the opened window fill the following information
```
Server Name: (LocalDb)\MSSQLLocalDB
Authentication: Windows Authentication
Database Name: CactusGuru.Database
```
Finally you can click the Publish button. Just remember that the mdf file must be placed in the bin/debug directory. You can detatch and attach the file again in order to do that. Or simply change the connection string (CactusGuru.Entry > app.config) so you don't have to move the mdf file.

### Using the Sample Database
Just download the mdf file from the [here](link) and attach it to your SQL Server.
Set the PROJECT_PATH before running the script.
```
USE [master]
CREATE DATABASE [CactusGuru.Database]
ON (FILENAME='PROJECT_PATH\CactusGuru.Entry\bin\Debug\CactusGuru.Database.mdf')
FOR ATTACH
```