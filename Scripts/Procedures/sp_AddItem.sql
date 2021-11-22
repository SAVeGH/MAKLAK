
--drop proc [dbo].[sp_AddItem]
CREATE PROCEDURE [dbo].[sp_AddItem]
@ItemType nvarchar(250),
@ItemId int,
@MeasureUnitId int,
@ItemValue nvarchar(500)
AS
BEGIN
	
	if(@ItemType = N'Name')
	begin
		insert into Product([Name])
		values(@ItemValue);
	end

	if(@ItemType = N'Property' OR @ItemType = N'PropertyValue')
	begin
		exec [dbo].[sp_AddPropertyItem] @Property_Id = @ItemId, @MeasureUnit_Id = @MeasureUnitId, @ItemValue = @ItemValue
		return
	end

	select @@IDENTITY;
END



--exec [dbo].[sp_AddItem] N'PropertyValue',1,2,'123'


GO





