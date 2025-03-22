CREATE TABLE [Categories] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Type] CHAR(7) CHECK ([Type] IN ('income', 'expense')),
    [UserId] INT NOT NULL,
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) ON DELETE CASCADE
);