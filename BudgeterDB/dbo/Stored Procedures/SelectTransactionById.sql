CREATE PROCEDURE [dbo].[SelectTransactionById]
	@Id INT
AS
	SELECT * FROM Transactions WHERE Id = @Id
RETURN 0
