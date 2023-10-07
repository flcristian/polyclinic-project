create table appointment(
    id integer auto_increment primary key,
    startDate datetime unique,
    endDate datetime unique
);

delete from appointment;
delete from appointment where id in (1,2,3);

select * from appointment;

insert into appointment values(1, '2023-10-07 12:00', '2023-10-07 13:00');
insert into appointment values(2, '2023-10-10 12:00', '2023-10-10 13:00');