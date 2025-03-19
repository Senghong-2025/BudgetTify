CREATE PROCEDURE [dbo].[CreateCategory_1.0] (
    @userId INT,
    @name NVARCHAR(100),
    @type CHAR(7)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Categories] ([Name], [Type], [UserId])
    VALUES (@name, @type, @userId);
    IF @@error <> 0
    BEGIN
        SELECT 1000 AS [ErrorCode], 'Create category failed' AS [ErrorMessage]
    END
    ELSE
    BEGIN
        SELECT 0 AS [ErrorCode], 'Success' AS [ErrorMessage]
    END
END
