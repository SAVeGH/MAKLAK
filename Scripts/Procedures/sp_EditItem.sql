USE [Srv]
GO

/****** Object:  StoredProcedure [dbo].[sp_AddItem]    Script Date: 09.01.2021 18:36:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc [dbo].[sp_EditItem]
CREATE PROCEDURE [dbo].[sp_EditItem]
@ItemType nvarchar(250),
@ItemId int,
@ItemValue nvarchar(500)
AS
BEGIN
	
	if(@ItemType = N'Name')
	begin
		update Product 
		set [Name] = @ItemValue
		where Id = @ItemId;
	end

	select @ItemId;
END
GO


