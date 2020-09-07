CREATE PROCEDURE [dbo].[SelectTransactionsByParent]
	@parentId INT,
	@transactionType INT
AS
	SELECT * FROM Transactions WHERE ParentId = @parentId AND TransactionType = @transactionType
RETURN 0
