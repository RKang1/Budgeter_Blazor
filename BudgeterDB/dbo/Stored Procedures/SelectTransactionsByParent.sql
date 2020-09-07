CREATE PROCEDURE [dbo].[SelectTransactionsByParent]
	@ParentId INT,
	@TransactionType INT
AS

IF @ParentId <= 0
BEGIN
	SET @ParentId = NULL
END

SELECT *
FROM Transactions
WHERE TransactionType = @TransactionType
AND (ParentId IS NULL OR ParentId = @ParentId)

RETURN 0
