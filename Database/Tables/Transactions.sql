CREATE TABLE [dbo].[Transactions] (
    [Id]                INT IDENTITY(1,1) PRIMARY KEY,
    [UserId]            INT NOT NULL,
    [CategoryId]        INT NOT NULL,
    [Amount]            DECIMAL(19,6) NOT NULL,
    [TransactionDate]   DATE NOT NULL,
    [Description]       NVARCHAR(MAX),
    [Status]            NVARCHAR(20) CHECK ([Status] IN ('pending', 'processing', 'completed')),
    [IsDeleted]         BIT DEFAULT 0,
    [DeletedAt]         DATETIME2,
    [CreatedAt]         DATETIME2 DEFAULT GETDATE(),
    [UpdatedAt]         DATETIME2 DEFAULT GETDATE()
);