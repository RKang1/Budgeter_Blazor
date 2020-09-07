CREATE PROCEDURE [dbo].[SelectTransaction]
	@transactionType INT
AS
	SELECT * FROM Transactions WHERE TransactionType = @transactionType
RETURN 0
