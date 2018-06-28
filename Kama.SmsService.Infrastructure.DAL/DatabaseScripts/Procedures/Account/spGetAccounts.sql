USE [Kama.SmsService]
GO

IF OBJECT_ID('pbl.spGetAccounts') IS NOT NULL
	DROP PROCEDURE pbl.spGetAccounts
GO

CREATE PROCEDURE pbl.spGetAccounts
	@AID UNIQUEIDENTIFIER
  , @AEnabled BIT
WITH ENCRYPTION 
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
		  , @Enabled BIT = @AEnabled
		  , @Result INT = 0

	IF @ID = pbl.EmptyGuid()
		SET @ID = NULL

	SELECT ID
			, Title
			, Domain
			, UserName
			, [Password]
			, Number
			, [Enabled]
			, [AlertCreditAmount]
			, [CreditAlertCount]
	FROM pbl.Account
	WHERE @Enabled is NULL OR  [Enabled] = @Enabled

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END