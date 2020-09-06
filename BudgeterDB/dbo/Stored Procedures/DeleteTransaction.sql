CREATE PROCEDURE [dbo].[DeleteTransaction]
	@Id Int
AS

DELETE Transactions
WHERE Id = @Id

RETURN 0
