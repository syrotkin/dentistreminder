Dentist reminder
Connection string: Data Source=test.db;Pooling=true;FailIfMissing=false

TODO:
- Screen for entering patients -- validation there
- Local country date format
- UI validation that the fields that are not null, are not null



CREATE TABLE Patient (
	PID INTEGER PRIMARY KEY,
	LastName TEXT(100) NOT NULL,
	FirstName TEXT(100),
	Patronymic TEXT(100),
	LastVisit TEXT(100),
	PhoneNumber TEXT(100)
);

insert into patient
(pid, lastname, firstname, patronymic, lastvisit, phonenumber)
values (2, 'Иваненко', 'Степан', null, '2016-03-01', null)

select pid, lastname, date(lastvisit)
from patient
where julianday('now') - julianday(lastvisit) >= 183