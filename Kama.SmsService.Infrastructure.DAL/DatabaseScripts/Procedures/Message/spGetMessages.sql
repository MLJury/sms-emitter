USE [Kama.SmsService]
GO

IF OBJECT_ID('msg.spGetMessages') IS NOT NULL
	DROP PROCEDURE msg.spGetMessages
GO

CREATE PROCEDURE msg.spGetMessages
	@ASourceAccountID UNIQUEIDENTIFIER
  , @AStatus SMALLINT
  , @AReceiverNumber VARCHAR(50)
  , @ASendDateFrom DATETIME
  , @ASendDateTo DATETIME
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @SourceAccountID UNIQUEIDENTIFIER = @ASourceAccountID 
		  , @Status SMALLINT = @AStatus
		  , @ReceiverNumber VARCHAR(50) = LTRIM(RTRIM(@AReceiverNumber))
		  , @SendDateFrom DATETIME = ISNULL(@ASendDateFrom, GETDATE())
		  , @SendDateTo DATETIME = ISNULL(@ASendDateTo, GETDATE())
		  , @Result INT = 0

	IF @SourceAccountID = NULL OR @SourceAccountID = pbl.EmptyGuid()
		RETURN 0

	SELECT m.ID
		 , m.ExternalMessageID
		 , m.SourceAccountID
		 , a.Number SourceAccountNumber
		 , a.Title SourceAccountTitle
		 , m.[Priority]
		 , m.SendType
		 , m.EncodingType
		 , m.[Status]
		 , m.SendDate
		 , m.DeliveryDate
		 , m.UDH
		 , m.Content
	FROM msg.[Message] m
	INNER JOIN pbl.Account a ON m.SourceAccountID = a.ID
	WHERE m.SourceAccountID = @SourceAccountID
		AND (@Status IS NULL OR m.[Status] = @Status)
		AND (@SendDateFrom IS NULL OR m.SendDate >= @SendDateFrom)
		AND (@SendDateTo IS NULL OR m.SendDate <= @SendDateTo)
	ORDER BY m.SendDate DESC, m.SourceAccountID

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END