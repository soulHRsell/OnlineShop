CREATE TABLE [dbo].[Product] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [Amount]     INT            DEFAULT ((0)) NOT NULL,
    [Info]       NVARCHAR (200) NULL,
    [CategoryId] INT            NOT NULL,
    [Price] DECIMAL NOT NULL, 
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([ID]) ON DELETE CASCADE
);


GO
