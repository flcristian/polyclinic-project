create table appointment(
    id integer auto_increment primary key,
    startDate datetime,
    endDate datetime
);

drop table appointment;

delete from appointment where id = 4;
delete from appointment where id in (1,2,3);
delete from appointment where id = 2;

select * from appointment;

insert into appointment values(1, '2023-10-26 12:00', '2023-10-26 13:00');
insert into appointment values(2, '2023-10-25 16:00', '2023-10-25 17:00');
insert into appointment values(3, '2023-10-26 14:00', '2023-10-26 15:00');

insert into appointment values(2, '2023-10-10 12:00', '2023-10-10 13:00');