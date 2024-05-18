create proc SaveUser
	@UserName nvarchar(20),
	@UserLastName nvarchar(20),
	@UserPhoneNumber nvarchar(20),
	@UserEmail nvarchar(20),
	@UserGender bit,
	@UserPassword nvarchar(20),
	@UserBirthDate date
	as
	insert into Users (UserName, UserLastName, UserPhoneNumber, UserEmail, UserGender, UserPassword, UserBirthDate)
	values (@UserName, @UserLastName, @UserPhoneNumber, @UserEmail, @UserGender, @UserPassword, @UserBirthDate)

	--exec saveuser ad soyad telefon email cinsiyet sifre dogumtarihi