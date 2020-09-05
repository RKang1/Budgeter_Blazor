CREATE PROCEDURE [dbo].[DeleteWantExpense]
	@Id Int
AS

DELETE WantExpense
WHERE Id = @Id

RETURN 0
