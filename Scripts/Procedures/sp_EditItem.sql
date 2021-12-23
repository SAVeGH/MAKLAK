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

	if(@ItemType = N'Property')
	begin
		update Property 
		set [Name] = @ItemValue
		where Id = @ItemId;
	end

	if(@ItemType = N'Tag')
	begin
		update Tag 
		set [Name] = @ItemValue
		where Id = @ItemId;
	end

	if(@ItemType = N'Category')
	begin
		update ProductCategory 
		set [Name] = @ItemValue
		where Id = @ItemId;
	end

	if(@ItemType = N'PropertyValue')
	begin
		
		declare @mutName nvarchar(50);

		select
		@mutName = mut.[Name] 
		from 
		 Property p
		 inner join MeasureUnits mu on p.MeasureUnit_Id = mu.Id
		 inner join MeasureUnitType mut on mu.MeasureUnitType_Id = mut.Id
		 inner join PropertyValue pv on pv.Property_Id = p.Id
		 where
		 pv.Id = @ItemId;
		 
		 update PropertyValue
		 set
		 digitValue = iif(@mutName in( N'Integer', N'Decimal'), cast(@ItemValue as decimal(28,8)), digitValue ),
		 stringValue = iif(@mutName = N'String', @ItemValue, stringValue),
		 boolValue = iif(@mutName = N'Boolean', cast(@ItemValue as bit), boolValue)
		 where
		 Id = @ItemId;	
	end

	select @ItemId;
END
GO


