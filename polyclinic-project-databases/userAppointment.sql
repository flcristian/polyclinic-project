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
select * from user_appointment;