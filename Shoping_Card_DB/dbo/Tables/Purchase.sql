CREATE TABLE [dbo].[Purchase] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [PurchaseDate] DATETIME2 (7) NOT NULL,
    [UserId]       INT           NOT NULL,
    [ProductId]    INT           NOT NULL,
    [IsCompleted] BIT NOT NULL DEFAULT 0, 
    [IsSent] BIT NOT NULL DEFAULT 0, 
    [Amount] INT NOT NULL DEFAULT 1, 
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Purchase_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ID]),
    CONSTRAINT [FK_Purchase_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([ID])
);

