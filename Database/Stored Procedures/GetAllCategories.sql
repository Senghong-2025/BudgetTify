CREATE PROCEDURE [dbo].[GetAllCategories_1.0] (
    @userId INT
)
AS 
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [Name], [Type], [UserId] FROM [dbo].[Categories] WITH (NOLOCK)
    WHERE [UserId] = @userId;

    IF @@error = 0
    BEGIN
        SELECT 0 AS [ErrorCode], 'Success' AS [ErrorMessage]
    END
    ELSE
    BEGIN
        SELECT 1000 AS [ErrorCode], 'Create category failed' AS [ErrorMessage]
    END
END