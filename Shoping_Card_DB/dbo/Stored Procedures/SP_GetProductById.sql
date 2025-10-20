CREATE PROCEDURE [dbo].[SP_GetProductById]
	@Id int
AS
begin

	set nocount on

	SELECT p.ID, p.[Name], p.Amount, p.Info, p.Price, p.CategoryId
	FROM Product p
	WHERE p.ID = @Id 
	ORDER BY [Name] ASC;

end
