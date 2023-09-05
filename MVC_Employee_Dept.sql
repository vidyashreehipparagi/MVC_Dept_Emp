create table Department
(
did int primary key identity(1,1),
dname varchar(20)
)


create table Employee
(
id int primary key identity(1,1),
name varchar(30),
salary numeric(12,2),
imageurl varchar(40),
did int
)


alter table Employee add imageurl varchar(40)
select * from Employee
select * from Department