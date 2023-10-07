create table user(
    id integer auto_increment primary key,
    name varchar(40),
    email varchar(40) unique,
    phone varchar(40) unique,
    type varchar(20)
);

delete from user where id in (1,2,3);
select * from user;