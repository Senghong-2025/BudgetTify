CREATE TABLE [Transactions] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [CategoryId] INT NULL,
    [Amount] DECIMAL(18,2) NOT NULL,
    [TransactionDate] DATE NOT NULL,
    [Description] NVARCHAR(MAX),
    [Type] CHAR(7) CHECK ([Type] IN ('income', 'expense')),
    [CreatedAt] DATETIME2 DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]) ON DELETE SET NULL
);