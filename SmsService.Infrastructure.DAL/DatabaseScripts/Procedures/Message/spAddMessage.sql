USE [SmsService]
GO

IF OBJECT_ID('msg.spAddMessage') IS NOT NULL
	DROP PROCEDURE msg.spAddMessage
GO

CREATE PROCEDURE msg.spAddMessage
	@AID UNIQUEIDENTIFIER
  , @ASourceAccountID UNIQUEIDENTIFIER
  , @AExternalMessageID BIGINT
  , @APriority TINYINT
  , @ASendType TINYINT
  , @AEncodingType TINYINT
  , @AStatus SMALLINT
  , @ASendDate DATETIME
  , @AUDH VARCHAR(50)
  , @AContent NVARCHAR(MAX)
  , @AReceiverNumbers NVARCHAR(max)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
		  , @SourceAccountID UNIQUEIDENTIFIER = @ASourceAccountID
		  , @ExternalMessageID BIGINT = @AExternalMessageID
		  , @Priority TINYINT = ISNULL(@APriority, 1)
		  , @SendType TINYINT = ISNULL(@ASendType, 0)
		  , @EncodingType TINYINT = ISNULL(@AEncodingType, 0)
		  , @Status SMALLINT = ISNULL(@AStatus, 0)
		  , @SendDate DATETIME = ISNULL(@ASendDate, GETDATE())
		  , @UDH VARCHAR(50) = LTRIM(RTRIM(@AUDH))
		  , @Content NVARCHAR(MAX) = LTRIM(RTRIM(@AContent))
		  , @ReceiverNumbers NVARCHAR(max) = @AReceiverNumbers
		  , @Result INT = 0
		  

	IF @ID = pbl.EmptyGuid()
		SET @ID = NULL

	BEGIN TRY
		BEGIN TRAN

			INSERT INTO msg.[Message]
			(ID, SourceAccountID, ExternalMessageID, [Priority], SendType, EncodingType, [Status], SendDate, DeliveryDate, UDH, Content)
			VALUES
			(@ID, @SourceAccountID, @ExternalMessageID, @Priority, @SendType, @EncodingType, @Status, @SendDate, NULL, @UDH, @Content)

			INSERT INTO msg.MessageReceiver
			SELECT 
			NEWID()
			, @ID
			, value
			, null
			, null
			, null
			, null
			FROM OPENJSON(@ReceiverNumbers)

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END