CREATE PROCEDURE [dbo].[SP_UpdatePurchaseQuantity]
	@id int,
	@quantity int
AS
begin
	
	set nocount on

	update Purchase
	set Amount = @quantity
	where ID = @id
		
end
