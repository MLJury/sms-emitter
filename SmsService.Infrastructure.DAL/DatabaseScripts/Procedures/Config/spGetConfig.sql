USE [SmsService]
GO

IF OBJECT_ID('pbl.spGetConfig') IS NOT NULL
	DROP PROCEDURE pbl.spGetConfig
GO

CREATE PROCEDURE pbl.spGetConfig
	@AName VARCHAR(256)
WITH ENCRYPTION 
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Name VARCHAR(256) = @AName
		  , @Result INT = 0

	SELECT ID, [Name], [Value]
	FROM pbl.Config
	WHERE [Name] = @Name

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END