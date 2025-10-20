CREATE PROCEDURE [dbo].[SP_GetProductByName]
	@name nvarchar(50)
AS
begin

	set nocount on

	SELECT p.ID, p.Name, p.Amount, p.Info, p.Price, p.CategoryId
	FROM dbo.Product p
	WHERE LOWER(p.[Name]) = LOWER(@name) 
	ORDER BY [Name] ASC;

end
