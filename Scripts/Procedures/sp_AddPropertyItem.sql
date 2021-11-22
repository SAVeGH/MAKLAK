
--drop proc [dbo].[sp_AddPropertyItem]
CREATE PROCEDURE [dbo].[sp_AddPropertyItem]
@Property_Id int,
@MeasureUnit_Id int,
@ItemValue nvarchar(500)
AS
BEGIN
	
	if(@Property_Id is NULL)
	begin
		insert into Property([Name], MeasureUnit_Id)
		values(@ItemValue, @MeasureUnit_Id);	

		--return @@IDENTITY;
	end
	else
	begin
		
		declare @mutName nvarchar(50);

		select @mutName = mut.[Name] 
		from
		MeasureUnitType mut
		inner join MeasureUnits mu on mut.Id = mu.MeasureUnitType_Id
		inner join Property p on p.MeasureUnit_Id = mu.Id
		where
		p.Id = @Property_Id;

		if(@mutName = 'Integer')
		begin
			insert into PropertyValue (Property_Id,digitValue)
			values(@Property_Id, cast(cast(@ItemValue as int) as decimal(28,8)));
		end
		else if(@mutName = 'Decimal')
		begin
			insert into PropertyValue (Property_Id,digitValue)
			values(@Property_Id, cast(@ItemValue as decimal(28,8)));
		end
		else if(@mutName = 'String')
		begin
			insert into PropertyValue (Property_Id,stringValue)
			values(@Property_Id, @ItemValue);
		end
		else if(@mutName = 'Boolean')
		begin
			insert into PropertyValue (Property_Id,boolValue)
			values(@Property_Id, cast(@ItemValue as bit));
		end
	end


	select @@IDENTITY;
END


--exec [dbo].[sp_AddPropertyItem] 1,1,'123'



GO


