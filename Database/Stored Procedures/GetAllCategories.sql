CREATE PROCEDURE [dbo].[GetAllCategories_1.0] (
    @userId INT
)
AS 
BEGIN
    SET NOCOUNT ON;

    SELECT c.[Id], c.[Name], c.[Type], u.[Id] AS [UserId], u.[Username]
        FROM [dbo].[Categories] c WITH (NOLOCK)
    INNER JOIN [dbo].[Users] u WITH (NOLOCK) 
        ON c.[UserId] = u.[Id]
    WHERE c.[UserId] = @userId;

    IF @@error = 0
    BEGIN
        SELECT 0 AS [ErrorCode], 'Success' AS [ErrorMessage]
    END
    ELSE
    BEGIN
        SELECT 1000 AS [ErrorCode], 'Retrieve category failed' AS [ErrorMessage]
    END
END