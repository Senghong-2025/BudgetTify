CREATE PROCEDURE [dbo].[UpdateTransaction_1.0] (
    @transactionId INT,
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

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Categories] WHERE [Id] = @categoryId AND [UserId] = @userId)
    BEGIN
        SELECT 1002 AS [ErrorCode], 'Invalid CategoryId' AS [ErrorMessage];
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Transactions] WHERE [Id] = @transactionId AND [UserId] = @userId)
    BEGIN
        SELECT 1003 AS [ErrorCode], 'Transaction not found or does not belong to the user' AS [ErrorMessage];
        RETURN;
    END

    UPDATE [dbo].[Transactions]
    SET
        [CategoryId] = @categoryId,
        [Amount] = @amount,
        [TransactionDate] = @transactionDate,
        [Description] = @description,
        [Status] = @status,
        [UpdatedAt] = GETDATE()
    WHERE [Id] = @transactionId AND [UserId] = @userId;

    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 0 AS [ErrorCode], 'Transaction updated successfully' AS [ErrorMessage];
    END
    ELSE
    BEGIN
        SELECT 1004 AS [ErrorCode], 'Transaction update failed' AS [ErrorMessage];
    END
END;
