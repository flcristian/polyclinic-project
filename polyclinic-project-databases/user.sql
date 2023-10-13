create table user(
    id integer auto_increment primary key,
    name varchar(40),
    email varchar(40) unique,
    phone varchar(40) unique,
    type varchar(20)
);

delete from user where id in (1,2,3);
select * from user;

select * from user;

insert into user values(1, "Andrei", "andrei@email.com","+1934812", "Patient");
insert into user values(2, "Marian", "marian@email.com","+1444812", "Patient");
insert into user values(3, "Marius", "marius1@email.com","+1666812", "Doctor");
insert into user values(4, "Marius", "marius2@email.com","+1631312", "Doctor");