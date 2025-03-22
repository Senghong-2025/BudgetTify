CREATE PROCEDURE [dbo].[GetTransaction_1.0](
    @userId INT
)
AS 
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.[Id], 
        t.[UserId], 
        u.[Username],
        t.[CategoryId], 
        c.[Name] AS CategoryName,
        t.[Amount], 
        t.[TransactionDate], 
        t.[Description], 
        t.[Status], 
        t.[CreatedAt], 
        t.[UpdatedAt]
    FROM 
        [dbo].[Transactions] t
    INNER JOIN 
        [dbo].[Categories] c ON t.[CategoryId] = c.[Id]
    INNER JOIN 
        [dbo].[Users] u ON t.[UserId] = u.[Id] 
    WHERE 
        t.[UserId] = @userId;

END;
