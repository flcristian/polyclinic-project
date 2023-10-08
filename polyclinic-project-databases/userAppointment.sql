create table userAppointment(
    id integer auto_increment primary key,
    pacientId integer,
    doctorId integer,
    appointmentId integer unique,
    foreign key (pacientId) references user(id) on delete cascade,
    foreign key (doctorId) references user(id) on delete cascade,
    foreign key (appointmentId) references appointment(id) on delete cascade
);

delete from userappointment where id in (1,2,3);
delete from user where id in (1,2,3);
delete from appointment where id in (1,2,3);
select * from userappointment;