CREATE PROCEDURE [dbo].[SP_GetUncompletedPurchasesByUserId]
	@userId int
AS
begin

	set nocount on 

	select *
	from Purchase
	where UserId = @userId and IsCompleted = 0
	ORDER BY ID ASC;

end
