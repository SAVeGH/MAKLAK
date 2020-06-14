USE [Srv]
GO

/****** Object:  StoredProcedure [dbo].[sp_RegisterUser]    Script Date: 14.06.2020 17:16:39 ******/
DROP PROCEDURE [dbo].[sp_RegisterUser]
GO

/****** Object:  StoredProcedure [dbo].[sp_RegisterUser]    Script Date: 14.06.2020 17:16:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_RegisterUser]
@UserName nvarchar(250),
@UserPassword nvarchar(50)
AS
BEGIN
	
	declare @UserId int = (select top 1 Id from Users where [Name] = @userName);

	if(@UserId  is  not NULL)
	begin
		select NULL;  -- already registered	
		return;
	end

	insert into Users ([Name], [Password])
	values (@UserName, @UserPassword);

	select SCOPE_IDENTITY();
END
GO


