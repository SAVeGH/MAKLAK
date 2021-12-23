--drop proc [dbo].[sp_DeleteItem]
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
	else if(@ItemType = N'Property')
	begin
		delete PropertyValue
		where Property_Id = @ItemId;

		delete Property
		where Id = @ItemId;
	end
	else if(@ItemType = N'PropertyValue')
	begin
		delete PropertyValue
		where Id = @ItemId;
	end
	else if(@ItemType = N'Tag')
	begin
		delete ProductTag
		where Tag_Id = @ItemId;

		delete Tag
		where Id = @ItemId;
	end
	else if (@ItemType = N'Category')
	begin

		declare @delCategories table(Id int);
		with
		delCategories as ( select Id from ProductCategory where Id = @ItemId
						   union all
        				   select pc.Id 
        				   from 
        				   ProductCategory pc
        				   inner join delCategories dc on pc.Parent_Id = dc.Id)
		insert into @delCategories
		select Id from delCategories;

		update ProductCategory
		set Parent_Id = NULL
		where Id in(select Id from @delCategories);

		delete ProductCategory
		where Id in(select Id from @delCategories);

	end


	select @ItemId;
END
GO


