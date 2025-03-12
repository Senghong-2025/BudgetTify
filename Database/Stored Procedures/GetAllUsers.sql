CREATE PROCEDURE GetAllUsers_1_0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [Username], [Email], [FirstName], [LastName], [CreatedAt], [UpdatedAt]
    FROM [Users] WITH (NOLOCK);
END