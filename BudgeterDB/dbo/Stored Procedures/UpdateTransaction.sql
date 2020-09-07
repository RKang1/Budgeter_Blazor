CREATE PROCEDURE [dbo].[UpdateTransaction]
	@Id Int,
	@PurchaseDate DATETIME2,
	@Description NVARCHAR(100),
	@Amount MONEY
AS

UPDATE Transactions 
SET	PurchaseDate = @PurchaseDate,
	Description = @Description,
	Amount = @Amount,
	RevisionDate = GETUTCDATE()
WHERE Id = @Id

RETURN 0
