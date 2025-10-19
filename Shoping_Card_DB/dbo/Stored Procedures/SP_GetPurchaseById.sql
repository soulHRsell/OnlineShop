CREATE PROCEDURE [dbo].[SP_GetPurchaseById]
	@Id int
AS
begin

	set nocount on 

	select *
	from Purchase
	where ID = @Id

end
