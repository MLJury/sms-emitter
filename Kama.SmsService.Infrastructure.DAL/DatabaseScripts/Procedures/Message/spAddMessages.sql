USE [Kama.SmsService]
GO

IF OBJECT_ID('msg.spAddMessages') IS NOT NULL
	DROP PROCEDURE msg.spAddMessages
GO

CREATE PROCEDURE msg.spAddMessages
	@AMessages nvarchar(max)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE 
			@Messages nvarchar(max) = @AMessages
		  , @Result INT = 0

	BEGIN TRY
		BEGIN TRAN

			INSERT INTO msg.[Message]
			SELECT 
			msgs.ID
			, msgs.SourceAccountID
			, msgs.ExternalMessageID
			, msgs.[Priority]
			, msgs.SendType
			, msgs.EncodingType
			, msgs.[Status]
			, msgs.SendDate
			, msgs.DeliveryDate
			, msgs.UDH
			, [Content]
			FROM OPENJSON(@Messages)
			WITH (
				ID UNIQUEIDENTIFIER
				, SourceAccountID UNIQUEIDENTIFIER
				, ExternalMessageID bigint
				, [Priority] tinyint
				, SendType tinyint
				, EncodingType tinyint
				, [Status] smallint
				, SendDate datetime
				, DeliveryDate datetime
				, UDH varchar(50)
				, [Content] nvarchar(MAX)  
			) as msgs

			INSERT INTO msg.MessageReceiver
			SELECT 
			NEWID()
			, recMsgs.ID
			, recMsgs.ReceiverNumbers
			, 0
			, null
			, 0
			, null
			FROM OPENJSON(@Messages)
			WITH (
				ID UNIQUEIDENTIFIER
				,  ReceiverNumbers nvarchar(max) AS JSON
			) as recMsgs


			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END