CREATE PROCEDURE [dbo].[SP_GetPurchase]
	@userId int,
	@productId int
AS
begin

	set nocount on

	select p.*
	from Purchase p
	where p.UserId = @userId and p.ProductId = @productId
	ORDER BY ID ASC;

end
