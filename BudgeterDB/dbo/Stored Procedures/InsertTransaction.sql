CREATE PROCEDURE [dbo].[InsertTransaction]
	@PurchaseDate DATETIME2,
	@Description NVARCHAR(100),
	@Amount MONEY
AS

INSERT INTO Transactions (PurchaseDate, Description, Amount, CreatedDate, RevisionDate)
VALUES (@PurchaseDate, @Description, @Amount, GETUTCDATE(), GETUTCDATE())

RETURN 0