CREATE PROCEDURE [dbo].[InsertUserLog_1.0] (
    @UserId INT,
    @IpAddress NVARCHAR(45) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [UserLog] ([UserId], [LoginTime], [IpAddress])
    VALUES (@UserId, GETDATE(), @IpAddress);
END