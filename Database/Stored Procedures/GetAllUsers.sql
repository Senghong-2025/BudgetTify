CREATE PROCEDURE [dbo].[GetAllUsers_1.0]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [Username], [Email], [FirstName], [LastName], [CreatedAt], [UpdatedAt]
    FROM [Users] WITH (NOLOCK);
END