create table Person (
	id_Person int primary key identity,
	FirstName nvarchar(150) not null,
	LastName nvarchar(150) not null
);

create table Employee (
	id_Employee int primary key identity,
	Email nvarchar(100) unique not null,
	[Password] nvarchar(100) not null,
	id_Person int references Person(id_Person) not null,
	Deleted bit not null default 0,
	DateCreated datetime not null default getdate()
);

create table EmployeeTask (
	id_EmployeeTask int primary key identity,
	id_Employee int references Employee(id_Employee) not null,
	[Description] nvarchar(500) not null,
	TimeSpent nvarchar(5) not null,
	DateCompleted date not null default getdate()
);
