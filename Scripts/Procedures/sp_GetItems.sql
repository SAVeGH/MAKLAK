--drop proc sp_GetItems
CREATE PROCEDURE [dbo].[sp_GetItems]
    @ItemId int,
	@ItemType nvarchar(20),
	@ItemValue nvarchar(100)
AS
BEGIN
	
	declare @result table (Id int, Parent_Id int, [Name] nvarchar(100))

	if(@ItemType = N'Name')
	begin
		insert into @result(Id, Parent_Id, [Name])
		select 
		Id,NULL,[Name] 
		from Product p
		where
		(@ItemId is NULL or p.Id = @ItemId) -- 2147483647
		and
		(isnull(@ItemValue,'') = N'' or [Name] like '%' + @ItemValue + '%');
	end
	else if(@ItemType = N'Vendor')
	begin
		insert into @result(Id, Parent_Id, [Name])
		select 
		Id,NULL,[Name] 
		from Users;
	end

	select * from @result;
END
GO

--exec sp_GetItems NULL,'Name', NULL