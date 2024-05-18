CREATE TABLE Employee (
EmployeeID int primary key identity,
EMainID uniqueidentifier not null,
EName nvarchar(20) not null,
ELastName nvarchar(20) not null,
EPhoneNumber nvarchar(20) not null,
EEmail nvarchar(20) not null,
EPassword nvarchar(20) not null,
EStartDate date default getdate(),
SaledCarsID int,
ERankID int
)