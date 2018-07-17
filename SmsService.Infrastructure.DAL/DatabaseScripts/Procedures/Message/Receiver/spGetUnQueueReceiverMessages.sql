USE [SmsService]
GO

IF OBJECT_ID('msg.spGetUnQueueReceiverMessages') IS NOT NULL
	DROP PROCEDURE msg.spGetUnQueueReceiverMessages
GO

CREATE PROCEDURE msg.spGetUnQueueReceiverMessages
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @Result INT = 0

	SELECT msgRec.ID
		 , msg.ID MessageID
		 , msg.ExternalMessageID
		 , msg.SourceAccountID
		 , account.Number SourceAccountNumber
		 , account.[Type] SourceAccount
		 , account.ID SourceAccountID
		 , msg.[Priority]
		 , msg.SendType
		 , msg.EncodingType
		 , msg.[Status]
		 , msg.SendDate
		 , msg.DeliveryDate
		 , msg.UDH
		 , msg.Content
		 , msgRec.IsQueue
		 , msgRec.IsSent
		 , msgRec.Number ReceiverNumber
	FROM msg.MessageReceiver msgRec
	INNER JOIN msg.[Message] msg ON msg.ID = msgRec.MessageID
	INNER JOIN pbl.Account account ON msg.SourceAccountID = account.ID
	WHERE msgRec.IsQueue is null
	ORDER BY msg.SendDate DESC, msg.SourceAccountID

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END