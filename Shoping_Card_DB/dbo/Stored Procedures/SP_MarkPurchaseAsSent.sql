CREATE PROCEDURE [dbo].[SP_MarkPurchaseAsSent]
    @Id INT
AS
BEGIN
    UPDATE Purchase
    SET IsSent = 1
    WHERE Id = @Id;
END
