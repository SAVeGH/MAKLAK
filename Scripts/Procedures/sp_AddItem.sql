
--drop proc [dbo].[sp_AddItem]
CREATE PROCEDURE [dbo].[sp_AddItem]
@ItemType nvarchar(250),
@ItemValue nvarchar(500)
AS
BEGIN
	
	if(@ItemType = N'Name')
	begin
		insert into Product([Name])
		values(@ItemValue);
	end

	select @@IDENTITY;
END
GO


