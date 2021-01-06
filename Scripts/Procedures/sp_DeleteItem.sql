--drop proc [dbo].[sp_AddItem]
CREATE PROCEDURE [dbo].[sp_DeleteItem]
@ItemType nvarchar(250),
@ItemId int
AS
BEGIN
	
	if(@ItemType = N'Name')
	begin
		delete Product
		where Id = @ItemId;
	end

	select @ItemId;
END
GO


