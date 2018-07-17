USE [Kama.SmsService]
GO

IF OBJECT_ID('pbl.spGetAccountAdminNumbers') IS NOT NULL
	DROP PROCEDURE pbl.spGetAccountAdminNumbers
GO

CREATE PROCEDURE pbl.spGetAccountAdminNumbers
	@AAccountID uniqueidentifier
WITH ENCRYPTION 
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @AccountID uniqueidentifier = @AAccountID
		  , @Result INT = 0

	SELECT 
	admNumbers.ID
	, admNumbers.AccountID
	, admNumbers.Number
	FROM pbl.AccountAdminNumber admNumbers
	inner join pbl.Account account on account.ID = admNumbers.AccountID
	WHERE account.ID = @AccountID

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END