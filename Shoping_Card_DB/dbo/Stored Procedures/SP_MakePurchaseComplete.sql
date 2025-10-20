CREATE PROCEDURE [dbo].[SP_MakePurchaseComplete]
	@Id int
AS
begin
	
	set nocount on 

	declare @productId int
	declare @purchaseAmount int

	select @productId = ProductId, @purchaseAmount = Amount 
	from Purchase
	where ID = @Id

	Update Purchase
	set IsCompleted = 1
	where ID = @Id

	Update Product
	set Amount = Amount - @purchaseAmount
	where ID = @productId
	
end
