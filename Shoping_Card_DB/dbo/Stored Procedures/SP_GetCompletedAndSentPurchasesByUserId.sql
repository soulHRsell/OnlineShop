CREATE PROCEDURE [dbo].[SP_GetCompletedAndSentPurchasesByUserId]
	@userId int
AS
begin
	
	set nocount on

	select * 
	from Purchase
	where UserId = @userId and IsCompleted = 1
	ORDER BY ID ASC;

end
