CREATE PROCEDURE [dbo].[SP_GetCategoryByName]
	@name nvarchar(50)
AS
begin
	
	set nocount on

	select *
	from Category ca
	where LOWER(ca.[Name]) = LOWER(@name)
	ORDER BY [Name] ASC;

end
