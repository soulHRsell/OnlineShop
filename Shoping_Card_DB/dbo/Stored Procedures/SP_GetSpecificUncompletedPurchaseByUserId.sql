CREATE PROCEDURE [dbo].[SP_GetSpecificUncompletedPurchaseByUserId]
	@userId int,
	@productId int
AS
begin

	set nocount on 

	select *
	from Purchase
	where UserId = @userId and ProductId = @productId and IsCompleted = 0
	ORDER BY ID ASC;

end
