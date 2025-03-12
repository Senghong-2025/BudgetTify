CREATE PROCEDURE [Register_1_0](
    @username NVARCHAR(100),
    @email NVARCHAR(150),
    @passwordHash NVARCHAR(225),
    @firstName NVARCHAR(100) = NULL,
    @lastName NVARCHAR(100) = NULL
)
AS
BEGIN 
    SET NOCOUNT ON;
    
    INSERT INTO [User] ([Username], [Email], [PasswordHash], [FirstName], [LastName])
    VALUES (@username, @email, @passwordHash, @firstName, @lastName);
END