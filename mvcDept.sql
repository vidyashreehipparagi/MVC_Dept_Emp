select * from Category
select * from Product
drop table product
create table product(
id int primary key identity(1,1),
name varchar(50),
price numeric(10,2),
imageUrl varchar(200),
Cid int,
constraint fk_cid foreign key(Cid) references Category(Cid)
)
sel