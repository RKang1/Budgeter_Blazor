CREATE PROCEDURE [dbo].[InsertTransaction]
	@ParentId INT,
	@TransactionType INT,
	@PurchaseDate DATETIME2,
	@Description NVARCHAR(100),
	@Amount MONEY
AS

IF @ParentId <= 0
BEGIN
	SET @ParentId = NULL
END

INSERT INTO Transactions (ParentId, TransactionType, PurchaseDate, Description, Amount, CreatedDate, RevisionDate)
VALUES (@ParentId, @TransactionType, @PurchaseDate, @Description, @Amount, GETUTCDATE(), GETUTCDATE())

RETURN 0