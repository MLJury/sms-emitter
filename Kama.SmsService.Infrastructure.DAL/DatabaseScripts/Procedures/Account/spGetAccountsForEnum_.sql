USE [Kama.SmsService]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetAccountsForEnum_'))
	DROP PROCEDURE pbl.spGetAccountsForEnum_
GO

CREATE PROCEDURE pbl.spGetAccountsForEnum_
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	select 
		acount.ID
		, acount.Title
	FROM pbl.Account acount

	RETURN @@ROWCOUNT
END