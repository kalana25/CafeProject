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
go
create table OrderHeader (
Id int identity(1,1) primary key,
OrderDate datetime not null,
SalesPerson varchar(250) null,
Amount decimal(10,2) not null,
OrderStatus varchar(50) not null
)
go
create table OrderProduct (
Id int identity(1,1) primary key,
OrderId int foreign key references OrderHeader(Id) not null,
ProductId int foreign key references Product(Id) not null,
Quantity int not null,
Total decimal(10,2) not null
)
go
create table OrderMenu (
Id int identity(1,1) primary key,
OrderId int foreign key references OrderHeader(Id) not null,
SetMenuId int foreign key references SetMenu(Id) not null,
Quantity int not null,
Total decimal(10,2) not null
)
--use master
--go
--drop database CafeDB
