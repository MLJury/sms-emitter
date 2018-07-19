USE [SMSService]
GO

IF OBJECT_ID('msg.spSetSendMessageReceiver') IS NOT NULL
	DROP PROCEDURE msg.spSetSendMessageReceiver
GO

CREATE PROCEDURE msg.spSetSendMessageReceiver
	@AID UNIQUEIDENTIFIER
	, @AIsSent BIT
	, @AMessage NVARCHAR(500)

WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
		  , @IsSent BIT = @AIsSent
		  , @Message NVARCHAR(500) = @AMessage
		  , @Result INT = 0 
		  , @SendDate DATETIME

		  set @SendDate = case @IsSent 
								when 0 then null 
								when 1 then GETDATE() 
								end 
	BEGIN TRY
		BEGIN TRAN
			
			update msg.MessageReceiver 
			set IsSent = @IsSent
				, SendDate = @SendDate  
			where ID = @ID

			insert into msg.SendTry (ID, ReceiverMessageID, [Date], Succeed, [Message])
			values(NewID(), @ID, GETDATE(), @IsSent, @Message)

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END