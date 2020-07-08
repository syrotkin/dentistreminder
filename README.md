# dentistreminder
An application for a dentist to enter patients' names, their visit dates, and receive reminders when the patient is due for the next appointment.

## Notes

Connection string: 
`Data Source=test.db;Pooling=true;FailIfMissing=false`


```sql
CREATE TABLE Patient (
	PID INTEGER PRIMARY KEY,
	LastName TEXT(100) NOT NULL,
	FirstName TEXT(100),
	Patronymic TEXT(100),
	LastVisit TEXT(100),
	PhoneNumber TEXT(100)
);
```

```sql
insert into patient
(pid, lastname, firstname, patronymic, lastvisit, phonenumber)
values (2, 'Иваненко', 'Степан', null, '2016-03-01', null)
```

```sql
select pid, lastname, date(lastvisit)
from patient
where julianday('now') - julianday(lastvisit) >= 183
```
