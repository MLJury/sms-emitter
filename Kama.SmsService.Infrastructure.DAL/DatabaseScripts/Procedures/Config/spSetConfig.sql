USE [Kama.SmsService]
GO

IF OBJECT_ID('pbl.spSetConfig') IS NOT NULL
	DROP PROCEDURE pbl.spSetConfig
GO

CREATE PROCEDURE pbl.spSetConfig
	@AData NVARCHAR(MAX) -- '[{"Name":"K1", "Value":"V1"}, {"Name":"K2", "Value":"V3"}]'
WITH ENCRYPTION 
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @JSON NVARCHAR(MAX) = @AData
		  , @Result INT = 0

	DECLARE @Items TABLE([Name] VARCHAR(256), [Value] NVARCHAR(1000))

	INSERT INTO @Items
	SELECT [Name], [Value]
	FROM OPENJSON(@JSON)
	WITH ([Name] VARCHAR(256), [Value] NVARCHAR(1000))

	BEGIN TRY
		BEGIN TRAN
			DELETE FROM pbl.Config
			WHERE EXISTS(SELECT 1 FROM @Items i WHERE i.[Name] = pbl.Config.[Name])

			INSERT INTO pbl.Config
			SELECT NEWID() ID
				 , i.[Name]
				 , i.[Value] 
			FROM @Items i

			SET @Result = @@ROWCOUNT
		COMMIT

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END