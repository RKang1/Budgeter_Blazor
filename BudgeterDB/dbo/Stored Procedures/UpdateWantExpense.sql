CREATE PROCEDURE [dbo].[UpdateWantExpense]
	@Id Int,
	@PurchaseDate DATETIME2,
	@Description NVARCHAR(100),
	@Amount MONEY
AS

UPDATE WantExpense
SET	PurchaseDate = @PurchaseDate,
	Description = @Description,
	Amount = @Amount,
	RevisionDate = GETUTCDATE()
WHERE Id = @Id

RETURN 0
