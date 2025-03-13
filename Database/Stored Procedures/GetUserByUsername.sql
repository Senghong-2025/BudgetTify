CREATE PROCEDURE [dbo].[GetUserByUsername_1.0](
    @username NVARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [Username], [Email], [FirstName], [LastName], [CreatedAt], [UpdatedAt]
    FROM [Users] WITH (NOLOCK)
    WHERE [Username] = @username;
END