CREATE PROCEDURE [dbo].[SP_UpdatePurchase]
	@Id int
AS
begin
	
	set nocount on

	Update Purchase 
	set Amount = Amount + 1
	where ID = @Id

end
