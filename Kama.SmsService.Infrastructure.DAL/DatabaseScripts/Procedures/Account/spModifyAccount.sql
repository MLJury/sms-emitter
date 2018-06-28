USE [Kama.SmsService]
GO

IF OBJECT_ID('pbl.spModifyAccount') IS NOT NULL
	DROP PROCEDURE pbl.spModifyAccount
GO

CREATE PROCEDURE pbl.spModifyAccount
	@AIsNewRecord BIT
  , @AID UNIQUEIDENTIFIER
  , @ATitle NVARCHAR(256)
  , @ADomain VARCHAR(1000)
  , @AUserName VARCHAR(50)
  , @APassword VARCHAR(256)
  , @ANumber VARCHAR(50)
  , @AEnabled BIT
  , @AAlertCreditAmount decimal
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @IsNewRecord BIT = ISNULL(@AIsNewRecord, 0)
		  , @ID UNIQUEIDENTIFIER = @AID
		  , @Title NVARCHAR(256) = LTRIM(RTRIM(@ATitle))
		  , @Domain VARCHAR(1000) = LTRIM(RTRIM(@ADomain))
		  , @UserName VARCHAR(50) = LTRIM(RTRIM(@AUserName))
		  , @Password VARCHAR(256) = LTRIM(RTRIM(@APassword))
		  , @Number VARCHAR(50) = LTRIM(RTRIM(@ANumber))
		  , @Enabled BIT = ISNULL(@AEnabled, 0)
		  , @AlertCreditAmount decimal = @AAlertCreditAmount
		  , @Result INT = 0


	BEGIN TRY
		BEGIN TRAN
			IF @IsNewRecord = 1
				INSERT INTO pbl.Account
				(ID, Title, Domain, UserName, [Password], Number, [Enabled], [AlertCreditAmount])
				VALUES
				(@ID, @Title, @Domain, @UserName, @Password, @Number, @Enabled, @AlertCreditAmount)
			ELSE
				UPDATE pbl.Account
				SET Title = @Title
				  , Domain = @Domain
				  , UserName = @UserName
				  , [Password] = @Password
				  , Number = @Number
				  , [Enabled] = @Enabled
				  , [AlertCreditAmount] = @AlertCreditAmount
				WHERE ID = @ID

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END