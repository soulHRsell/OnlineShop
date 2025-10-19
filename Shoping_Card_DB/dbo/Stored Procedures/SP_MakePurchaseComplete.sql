CREATE PROCEDURE [dbo].[SP_MakePurchaseComplete]
	@Id int
AS
begin
	
	set nocount on 

	Update Purchase
	set IsCompleted = 1
	where ID = @Id

end
