CREATE PROCEDURE [dbo].[SP_GetPurchaseByUserId]
	@userId int
AS
begin
	
	set nocount on

	select *
	from Purchase
	where UserId = @userId
	
end
