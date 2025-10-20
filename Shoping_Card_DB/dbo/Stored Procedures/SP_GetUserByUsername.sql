CREATE PROCEDURE [dbo].[SP_GetUserByUsername]
	@username nvarchar(50)
AS
begin
	
	set nocount on;

	select u.ID, u.Username, u.[Password], u.isAdmin
	from [User] u
	where u.Username = @username
	ORDER BY Username ASC;

end
