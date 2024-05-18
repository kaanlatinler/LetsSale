create table Notify(
	NotifyID int identity primary key,
	CarID uniqueidentifier,
	UserID uniqueidentifier
)