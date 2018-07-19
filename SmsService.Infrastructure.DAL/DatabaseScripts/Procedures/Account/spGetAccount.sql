USE [SMSService]
GO

IF OBJECT_ID('pbl.spGetAccount') IS NOT NULL
	DROP PROCEDURE pbl.spGetAccount
GO

CREATE PROCEDURE pbl.spGetAccount
	@AID UNIQUEIDENTIFIER
WITH ENCRYPTION 
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
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
	WHERE @ID is NULL OR  ID = @ID

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END