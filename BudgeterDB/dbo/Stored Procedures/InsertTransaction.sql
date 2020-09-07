CREATE PROCEDURE [dbo].[InsertTransaction]
	@TransactionType INT,
	@PurchaseDate DATETIME2,
	@Description NVARCHAR(100),
	@Amount MONEY
AS

INSERT INTO Transactions (TransactionType, PurchaseDate, Description, Amount, CreatedDate, RevisionDate)
VALUES (@TransactionType, @PurchaseDate, @Description, @Amount, GETUTCDATE(), GETUTCDATE())

RETURN 0