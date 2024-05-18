create table ServiceCars(
	ServiceID int identity primary key,
	SMainID uniqueidentifier,
	SCarID uniqueidentifier,
	SUserID uniqueidentifier,
	SEmployeeID uniqueidentifier,
	SDesc text ,
	SstartDate datetime default getdate(),
	SfinishDate datetime default getdate(),
	IsFinish bit
)