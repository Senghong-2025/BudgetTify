CREATE PROCEDURE [dbo].[Register_1.0](
    @username NVARCHAR(100),
    @email NVARCHAR(150),
    @passwordHash NVARCHAR(225),
    @firstName NVARCHAR(100) = NULL,
    @lastName NVARCHAR(100) = NULL
)
AS
BEGIN 
    SET NOCOUNT ON;
    
    INSERT INTO [Users] ([Username], [Email], [PasswordHash], [FirstName], [LastName])
    VALUES (@username, @email, @passwordHash, @firstName, @lastName);
    
    SELECT [Id] AS [UserId], [Username], [Email], [FirstName], [LastName]
    FROM [dbo].[Users] WITH (NOLOCK)
    WHERE [Username] = @username AND [Email] = @email;
END