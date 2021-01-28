create database CafeDB
go
use CafeDB
go
create table Product(
Id int identity(1,1) primary key,
Code varchar(50) not null,
Name varchar(250) not null,
Price decimal(10,2) null,
[Description] varchar(500),
[Type] varchar(50) not null
)
go
create table SetMenu(
Id int identity(1,1) primary key,
Code varchar(50) not null,
Name varchar(250) not null,
Price decimal(10,2) null,
)
go
create table SetMenuItem(
Id int identity(1,1) primary key,
ProductId int foreign key references Product(Id) not null,
SetMenuId int foreign key references SetMenu(Id) not null,
Quantity int not null
)
go
create table MenuDiscount(
Id int identity(1,1) primary key,
SetMenuId int foreign key references SetMenu(Id) not null,
Rate decimal(10,2) not null,
StartDate datetime not null,
EndDate datetime not null
)

--use master
--go
--drop database CafeDB