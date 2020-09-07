﻿CREATE TABLE [dbo].[Transactions] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [ParentId]        INT           NULL,
    [TransactionType] INT           NULL,
    [PurchaseDate]    DATETIME2 (7) NULL,
    [Description]     NVARCHAR (50) NULL,
    [Amount]          MONEY         NULL,
    [CreatedDate]     DATETIME2 (7) NULL,
    [RevisionDate]    DATETIME2 (7) NULL,
    CONSTRAINT [PK__tmp_ms_x__3214EC075B485228] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Transactions_Transactions] FOREIGN KEY ([ParentId]) REFERENCES [Transactions]([Id]) 
);


