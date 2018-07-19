USE [SMSService]
GO

IF OBJECT_ID('msg.spSetQueueMessageReceivers') IS NOT NULL
	DROP PROCEDURE msg.spSetQueueMessageReceivers
GO

CREATE PROCEDURE msg.spSetQueueMessageReceivers
	@AIDs NVARCHAR(Max),
	@AIsQueue BIT

WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @IDs NVARCHAR(Max) = @AIDs
		  , @IsQueue BIT = @AIsQueue
		  , @QueueDate SMALLDATETIME
		  , @Result INT = 0

		  set @QueueDate = case @IsQueue 
								when 0 then null 
								when 1 then GETDATE() 
								end
	BEGIN TRY
		BEGIN TRAN
			
			update msg.MessageReceiver 
			set IsQueue = @IsQueue
				, QueueDate = @QueueDate 
			from openjson(@IDs)
			where ID = value

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END