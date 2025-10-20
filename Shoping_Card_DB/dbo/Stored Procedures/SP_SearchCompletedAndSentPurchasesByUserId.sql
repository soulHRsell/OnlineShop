CREATE PROCEDURE [dbo].[SP_SearchCompletedAndSentPurchasesByUserId]
    @Id INT = NULL,
    @productName NVARCHAR(50) = NULL,
    @status NVARCHAR(20) = NULL,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT pu.*
    FROM Purchase pu
    INNER JOIN Product p ON p.ID = pu.ProductId
    WHERE 
        pu.UserId = @UserId
        AND (@Id IS NULL OR pu.ID = @Id)
        AND (@productName IS NULL OR LOWER(p.[Name]) LIKE '%' + LOWER(@productName) + '%')
        AND (
            @status IS NULL
            OR (
                @status = 'Completed' AND pu.IsCompleted = 1 AND pu.IsSent = 0
            )
            OR (
                @status = 'Sent' AND pu.IsSent = 1
            )
            OR (
                @status = 'Pending' AND pu.IsCompleted = 0 AND pu.IsSent = 0
            )
        )
    ORDER BY pu.PurchaseDate DESC;

end
