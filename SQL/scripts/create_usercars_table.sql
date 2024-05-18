create table UserCars (
	CarID int primary key identity,
	CMainID uniqueidentifier not null,
	CarName nvarchar(20) not null,
	CarBrand nvarchar(20) not null,
	CarPlateNumber nvarchar(20) not null,
	CarYear int,
	CarSaleDate datetime default getdate(),
	UserID uniqueidentifier
)