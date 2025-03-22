CREATE PROCEDURE [dbo].[AddUserWallet_1.0] (
    @userId INT,
    @currency CHAR(3) = 'USD'
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM [dbo].[UserWallets] WHERE [UserId] = @userId AND [Currency] = @currency)
    BEGIN
        SELECT 1002 AS [ErrorCode], 'User already has a wallet with the specified currency' AS [ErrorMessage];
        RETURN;
    END

    INSERT INTO [dbo].[UserWallets] (
        [UserId], 
        [Balance], 
        [Currency], 
        [CreatedAt], 
        [UpdatedAt], 
        [IsActive]
    )
    VALUES (
        @userId, 
        0.00,
        @currency,
        GETDATE(), 
        GETDATE(), 
        1 
    );

    SELECT 0 AS [ErrorCode], 'User wallet created successfully' AS [ErrorMessage];
END;
