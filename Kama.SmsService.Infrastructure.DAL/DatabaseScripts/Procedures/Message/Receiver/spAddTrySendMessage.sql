USE [Kama.SmsService]
GO

IF OBJECT_ID('msg.spAddTrySendMessage') IS NOT NULL
	DROP PROCEDURE msg.spAddTrySendMessage
GO

CREATE PROCEDURE msg.spAddTrySendMessage
	@AMessageReceiverID UNIQUEIDENTIFIER
	, @AMessage nvarchar(500)

WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @MessageReceiverID UNIQUEIDENTIFIER = @AMessageReceiverID
			, @Message nvarchar(500) = @AMessage
			, @Result INT = 0

	BEGIN TRY
		BEGIN TRAN
			insert into msg.SendTry(ID, ReceiverMessageID, [Date], Succeed, [Message])
			values (NewID(), @MessageReceiverID, GETDATE(), 0, @Message)

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END