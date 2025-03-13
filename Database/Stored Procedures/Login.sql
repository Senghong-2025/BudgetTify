CREATE PROCEDURE [dbo].[Login_1.0] (
    @usernameOrEmail NVARCHAR(100),
    @passwordHash NVARCHAR(225),
    @ipAddress NVARCHAR(45) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @userId INT;
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WITH (NOLOCK) 
    WHERE [Username] = @usernameOrEmail OR [Email] = @usernameOrEmail)
    BEGIN
        SELECT -1 AS [UserId];
        RETURN;
    END
    SELECT 
        @userId = [Id]
    FROM 
        [dbo].[Users] WITH (NOLOCK)
    WHERE 
        ([Username] = @usernameOrEmail OR [Email] = @usernameOrEmail) 
        AND [PasswordHash] = @passwordHash;

    IF @userId IS NOT NULL
    BEGIN
        SELECT 
            [Id] AS [UserId],
            [Username], 
            [Email], 
            [FirstName], 
            [LastName],
            0 AS [ErrorCode]
        FROM 
            [dbo].[Users] WITH (NOLOCK)
        WHERE 
            [Id] = @userId;

        EXEC [dbo].[InsertUserLog_1.0] @UserId = @userId, @IpAddress = @ipAddress;
    END
    ELSE
    BEGIN
        SELECT 3003 AS [ErrorCode];
    END;

END