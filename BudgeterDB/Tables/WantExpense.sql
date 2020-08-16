CREATE TABLE [dbo].[WantExpense]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PurchaseDate] DATETIME2 NULL, 
    [Description] NVARCHAR(50) NULL, 
    [Amount] MONEY NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [RevisionDate] DATETIME2 NULL
)
