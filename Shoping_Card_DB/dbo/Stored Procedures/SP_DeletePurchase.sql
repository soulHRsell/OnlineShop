CREATE PROCEDURE [dbo].[SP_DeletePurchase]
	@Id int
AS
begin
	
	set nocount on 

	Delete from Purchase where ID = @Id

end
