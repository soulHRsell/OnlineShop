CREATE PROCEDURE [dbo].[SP_CreateNewPurchase]
	@userId int,
	@productId int
AS
begin
	
	set nocount on

	Insert into Purchase(PurchaseDate, UserId, ProductId) values (GETDATE(), @userId, @productId)

end
