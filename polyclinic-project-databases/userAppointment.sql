create table user_appointment(
    id integer auto_increment primary key,
    patientId integer,
    doctorId integer,
    appointmentId integer unique,
    foreign key (patientId) references user(id) on delete cascade,
    foreign key (doctorId) references user(id) on delete cascade,
    foreign key (appointmentId) references appointment(id) on delete cascade
);

drop table userAppointment;

delete from user_appointment where id in (1,2,3);
delete from user where id in (1,2,3);
delete from appointment where id in (1,2,3);
delete from user_appointment where id = 2;
select * from user_appointment;

insert into user_appointment values (1,1,3,1);
insert into user_appointment values (2,1,3,2);
insert into user_appointment values (3,1,3,3);