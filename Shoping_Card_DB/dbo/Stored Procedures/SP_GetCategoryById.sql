CREATE PROCEDURE [dbo].[SP_GetCategoryById]
	@Id int
AS
begin
	
	set nocount on

	Select ca.ID, ca.[Name]
	From Category ca
	Where ca.ID = @Id
	ORDER BY [Name] ASC;

end
