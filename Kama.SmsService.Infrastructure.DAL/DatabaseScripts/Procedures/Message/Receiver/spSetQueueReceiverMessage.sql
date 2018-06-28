USE [Kama.SmsService]
GO

IF OBJECT_ID('msg.spSetQueueMessageReceiver') IS NOT NULL
	DROP PROCEDURE msg.spSetQueueMessageReceiver
GO

CREATE PROCEDURE msg.spSetQueueMessageReceiver
	@AID UNIQUEIDENTIFIER,
	@AIsQueue BIT

WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
		  , @IsQueue BIT = @AIsQueue
		  , @Result INT = 0

	BEGIN TRY
		BEGIN TRAN
			
			update msg.MessageReceiver 
			set IsQueue = @IsQueue
				, QueueDate = case @IsQueue 
								when 0 then null 
								when 1 then GETDATE() 
								end  
			where ID = @ID

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END