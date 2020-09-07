CREATE PROCEDURE [dbo].[SelectTransactions]
	@transactionType INT
AS
	SELECT * FROM Transactions WHERE TransactionType = @transactionType
RETURN 0
