CREATE PROCEDURE [dbo].[UpdateUserWallet_1.0] (
    @UserId INT,
    @Currency CHAR(3),
    @Amount DECIMAL(19,6),
    @IsActive BIT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[UserWallets] WHERE [UserId] = @UserId AND [Currency] = @Currency)
    BEGIN
        SELECT 1003 AS [ErrorCode], 'No wallet found for this user with the specified currency' AS [ErrorMessage];
        RETURN;
    END

    UPDATE [dbo].[UserWallets]
    SET
        [Balance] = [Balance] + @Amount,
        [UpdatedAt] = GETDATE(),
        [IsActive] = COALESCE(@IsActive, [IsActive])
    WHERE 
        [UserId] = @UserId AND
        [Currency] = @Currency;

    SELECT 0 AS [ErrorCode], 'Wallet updated successfully' AS [ErrorMessage];
END;
