USE [Srv]
GO

/****** Object:  StoredProcedure [dbo].[sp_AddItem]    Script Date: 06.12.2021 15:56:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



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

	if(@ItemType = N'Tag')
	begin
		insert into Tag([Name])
		values(@ItemValue);
	end

	if(@ItemType = N'Property' OR @ItemType = N'PropertyValue')
	begin
		exec [dbo].[sp_AddPropertyItem] @Property_Id = @ItemId, @MeasureUnit_Id = @MeasureUnitId, @ItemValue = @ItemValue
		return
	end

	if(@ItemType = N'Category')
	begin
		insert into ProductCategory(Parent_Id, [Name])
		values(@ItemId, @ItemValue);
	end


	select @@IDENTITY;
END



--exec [dbo].[sp_AddItem] N'PropertyValue',1,2,'123'


GO


