create table CarPics (
id int identity primary key ,
CarPicsPath nvarchar(max) not null,
CarID uniqueidentifier
)