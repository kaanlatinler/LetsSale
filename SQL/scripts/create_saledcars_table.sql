CREATE TABLE SaledCars (
SaledCarsID int identity primary key,
SCMainID uniqueidentifier not null,
SCarName nvarchar(20) not null,
SCarBrand nvarchar(20) not null,
SCarSaleDate datetime default getdate(),
SCarPrice nvarchar(20) not null,
SCarPlateNumber nvarchar(20) not null,
EmployeeID uniqueidentifier,
UserId uniqueidentifier,
SCarCategoryID int,
SCarMainPic nvarchar(max)
)