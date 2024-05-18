create table Users (
	UserID int primary key identity,
	UMainID uniqueidentifier not null,
	UserName nvarchar(20) not null,
	UserLastName nvarchar(20) not null,
	UserPhoneNumber nvarchar(20) not null,
	UserEmail nvarchar(50) unique not null,
	UserPassword nvarchar(20) not null,
	UserRegisterDate datetime default getdate(),
	UserCarsID uniqueidentifier default '5C45C45C-CDCE-4BD6-83CC-F63C0FEFC35F'
)