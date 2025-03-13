CREATE PROCEDURE [dbo].[Login_1.0] (
    @usernameOrEmail NVARCHAR(100),
    @passwordHash NVARCHAR(225),
    @ipAddress NVARCHAR(45) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @userId INT;

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
            [LastName]
        FROM 
            [dbo].[Users] WITH (NOLOCK)
        WHERE 
            [Id] = @userId;

        EXEC [dbo].[InsertUserLog_1.0] @UserId = @userId, @IpAddress = @ipAddress;
    END
END