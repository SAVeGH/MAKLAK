USE [Srv]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetUser]    Script Date: 14.06.2020 17:14:54 ******/
DROP PROCEDURE [dbo].[sp_GetUser]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetUser]    Script Date: 14.06.2020 17:14:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_GetUser]
@UserName nvarchar(250),
@UserPassword nvarchar(50)
AS
BEGIN
	
	declare @UserId int = (select top 1 Id from Users where [Name] = @userName and [Password] = @UserPassword);   

	select @UserId;
END
GO


