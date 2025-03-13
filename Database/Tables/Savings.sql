CREATE TABLE Savings (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [GoalName] NVARCHAR(100) NOT NULL,
    [TargetAmount] DECIMAL(18,2) NOT NULL,
    [CurrentAmount] DECIMAL(18,2) DEFAULT 0,
    [Deadline] DATE NULL,
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) ON DELETE CASCADE
);
