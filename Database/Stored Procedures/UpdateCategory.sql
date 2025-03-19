CREATE PROCEDURE [dbo].[UpdateCategory_1.0] (
    @id INT,
    @userId INT,
    @name NVARCHAR(100),
    @type CHAR(7)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Categories]
    SET [Name] = @name, 
        [Type] = @type, 
        [UserId] = @userId
    WHERE [Id] = @id;

    IF @@ROWCOUNT = 0
    BEGIN
        SELECT 1001 AS [ErrorCode], 'Update category failed or no changes made' AS [ErrorMessage];
    END
    ELSE
    BEGIN
        SELECT 0 AS [ErrorCode], 'Success' AS [ErrorMessage];
    END
END;
