CREATE PROCEDURE [dbo].[InsertWantExpense]
	@PurchaseDate DATETIME2,
	@Description NVARCHAR(100),
	@Amount MONEY
AS

INSERT INTO WantExpense (PurchaseDate, Description, Amount, CreatedDate, RevisionDate)
VALUES (@PurchaseDate, @Description, @Amount, GETUTCDATE(), GETUTCDATE())

RETURN 0