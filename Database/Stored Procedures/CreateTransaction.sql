CREATE PROCEDURE [dbo].[CreateTransaction_1.0] (
    @userId INT,
    @categoryId INT,
    @amount DECIMAL(19,6),
    @transactionDate DATE,
    @description NVARCHAR(MAX),
    @status NVARCHAR(20)
)
AS 
BEGIN
    SET NOCOUNT ON;

     IF NOT EXISTS (SELECT 1 FROM [dbo].[Categories] WHERE [Id] = @CategoryId AND [UserId] = @userId)
    BEGIN
        SELECT 1002 AS [ErrorCode], 'Invalid CategoryId' AS [ErrorMessage];
        RETURN;
    END

    INSERT INTO [dbo].[Transactions] (
        [UserId], 
        [CategoryId], 
        [Amount], 
        [TransactionDate], 
        [Description], 
        [Status],
        [CreatedAt],
        [UpdatedAt]
    )
    VALUES (
        @UserId, 
        @CategoryId, 
        @Amount, 
        @TransactionDate, 
        @Description, 
        @Status,
        GETDATE(),
        GETDATE()
    );

    SELECT 0 AS [ErrorCode], 'Transaction inserted successfully' AS [ErrorMessage];
END;