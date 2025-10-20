CREATE PROCEDURE [dbo].[SP_AdminSearchPurchases]
 @Id INT = NULL,
    @productName NVARCHAR(50) = NULL,
    @status NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT pu.*, u.Username, p.Name AS ProductName, c.Name AS CategoryName, p.Price
    FROM Purchase pu
    INNER JOIN Product p ON pu.ProductId = p.Id
    INNER JOIN Category c ON p.CategoryId = c.Id
    INNER JOIN [User] u ON pu.UserId = u.Id
    WHERE 
        (@Id IS NULL OR pu.Id = @Id)
        AND (@productName IS NULL OR LOWER(p.Name) LIKE '%' + LOWER(@productName) + '%')
        AND (
            @status IS NULL
            OR (@status = 'Completed' AND pu.IsCompleted = 1 AND pu.IsSent = 0)
            OR (@status = 'Sent' AND pu.IsSent = 1)
            OR (@status = 'Pending' AND pu.IsCompleted = 0)
        )
    ORDER BY pu.PurchaseDate DESC;
END
