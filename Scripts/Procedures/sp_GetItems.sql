USE [Srv]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetItems]    Script Date: 06.12.2021 16:03:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc sp_GetItems
CREATE PROCEDURE [dbo].[sp_GetItems]
    @ItemId int,
	@ParentItemId int,
	@ItemType nvarchar(20),
	@ItemValue nvarchar(100)
AS
BEGIN
	
	declare @result table (Id int, Parent_Id int, ItemType nvarchar(20), [Name] nvarchar(100), MeasureUnit_Id int, HasChildren bit)

	if(@ItemType = N'Name')
	begin
		insert into @result(Id, Parent_Id, ItemType, [Name])
		select 
		Id,NULL,@ItemType,[Name]
		from Product p
		where
		(@ItemId is NULL or p.Id = @ItemId) -- 2147483647
		and
		(isnull(@ItemValue,'') = N'' or [Name] like '%' + @ItemValue + '%');
	end
	else if(@ItemType = N'Vendor')
	begin
		insert into @result(Id, Parent_Id, ItemType, [Name])
		select 
		Id,NULL, @ItemType, [Name] 
		from Users
		where		
		(isnull(@ItemValue,'') = N'' or [Name] like '%' + @ItemValue + '%');
	end
	else if(@ItemType = N'Property')
	begin
		
		if(@ParentItemId is NULL) -- NULL только в случае выборки свойств
		begin
			insert into @result(Id, Parent_Id, ItemType, [Name], MeasureUnit_Id, HasChildren)
			select 
			p.Id,NULL, @ItemType, p.[Name], p.MeasureUnit_Id, iif(pve.Id is NULL, 0, 1) 
			from 
			Property p
			outer apply( select top 1 pv.Id from PropertyValue pv where pv.Property_Id = p.Id) as pve
			where	
			(@ItemId is NULL or p.Id = @ItemId)
			and	
			(isnull(@ItemValue,'') = N'' or [Name] like '%' + @ItemValue + '%');
		end
		else
		begin

			insert into @result(Id, Parent_Id, ItemType, [Name])
			select
			pv.Id, 
			p.Id, -- Id свойства в качестве Parent_Id
			N'PropertyValue',
			( case mut.[Name]
				when 'Integer' then cast(cast(pv.digitValue as int) as nvarchar(20))
				when 'Decimal' then cast(pv.digitValue as nvarchar(20))
				when 'Boolean' then cast(pv.boolValue as nvarchar(20))
				when 'String' then pv.stringValue
			   end
			 ) as [Name]
			 from 
			 Property p
			 inner join MeasureUnits mu on p.MeasureUnit_Id = mu.Id
			 inner join MeasureUnitType mut on mu.MeasureUnitType_Id = mut.Id
			 inner join PropertyValue pv on pv.Property_Id = p.Id
			 where
			 p.Id = @ParentItemId;			 

		end
		
	end
	else if(@ItemType = N'PropertyValue')
	begin
		insert into @result(Id, Parent_Id, ItemType, [Name])
		select
		pv.Id, 
		p.Id, -- Id свойства в качестве Parent_Id
		N'PropertyValue',
		( case mut.[Name]
			when 'Integer' then cast(cast(pv.digitValue as int) as nvarchar(20))
			when 'Decimal' then cast(pv.digitValue as nvarchar(20))
			when 'Boolean' then cast(pv.boolValue as nvarchar(20))
			when 'String' then pv.stringValue
		   end
		 ) as [Name]
		 from 
		 Property p
		 inner join MeasureUnits mu on p.MeasureUnit_Id = mu.Id
		 inner join MeasureUnitType mut on mu.MeasureUnitType_Id = mut.Id
		 inner join PropertyValue pv on pv.Property_Id = p.Id
		 where
		 (@ItemId is NULL or pv.Id = @ItemId);	
	end
	else if(@ItemType = N'Tag')
	begin
		insert into @result(Id, Parent_Id, ItemType, [Name])
		select 
		Id,NULL,@ItemType,[Name]
		from Tag t
		where
		(@ItemId is NULL or t.Id = @ItemId) -- 2147483647
		and
		(isnull(@ItemValue,'') = N'' or [Name] like '%' + @ItemValue + '%');
	end
	else if(@ItemType = N'Category')
	begin
		insert into @result(Id, Parent_Id, ItemType, [Name], HasChildren)
		select 
		pc.Id,pc.Parent_Id,@ItemType,pc.[Name], iif(pcq.Id is NULL, 0, 1)
		from ProductCategory pc
		outer apply( select top 1 pcs.Id from ProductCategory pcs where pcs.Parent_Id = pc.Id) as pcq
		where
		(@ItemId is NULL or pc.Id = @ItemId) -- 2147483647
		and
		(@ParentItemId is NULL or pc.Parent_Id = @ParentItemId)
		and
		(isnull(@ItemValue,'') = N'' or pc.[Name] like '%' + @ItemValue + '%');
	end
	

	select * from @result;
END



GO


