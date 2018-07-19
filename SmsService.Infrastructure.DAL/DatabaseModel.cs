using System;
using DatabaseModel;
using System.Threading.Tasks;

namespace SmsService.Infrastructure.DAL
{
class MSG: Database
{
#region Constructors
public MSG(string connectionString)
	:base(connectionString){}

public MSG(string connectionString, IModelValueBinder modelValueBinder)
	:base(connectionString, modelValueBinder){}
#endregion

#region SetQueueMessageReceiver

public System.Data.SqlClient.SqlCommand GetCommand_SetQueueMessageReceiver(Guid? _id, bool? _isQueue)
{
return base.CreateCommand("msg.spSetQueueMessageReceiver", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AIsQueue", IsOutput = false, Value = _isQueue == null ? DBNull.Value : (object)_isQueue }, 
	});

}

public async Task<ResultSet> SetQueueMessageReceiverAsync(Guid? _id, bool? _isQueue)
{
	using(var cmd = GetCommand_SetQueueMessageReceiver(_id, _isQueue))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet SetQueueMessageReceiver(Guid? _id, bool? _isQueue)
{
	using(var cmd = GetCommand_SetQueueMessageReceiver(_id, _isQueue))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region SetQueueMessageReceivers

public System.Data.SqlClient.SqlCommand GetCommand_SetQueueMessageReceivers(string _iDs, bool? _isQueue)
{
return base.CreateCommand("msg.spSetQueueMessageReceivers", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AIDs", IsOutput = false, Value = string.IsNullOrWhiteSpace(_iDs) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_iDs) }, 
					new Parameter { Name = "@AIsQueue", IsOutput = false, Value = _isQueue == null ? DBNull.Value : (object)_isQueue }, 
	});

}

public async Task<ResultSet> SetQueueMessageReceiversAsync(string _iDs, bool? _isQueue)
{
	using(var cmd = GetCommand_SetQueueMessageReceivers(_iDs, _isQueue))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet SetQueueMessageReceivers(string _iDs, bool? _isQueue)
{
	using(var cmd = GetCommand_SetQueueMessageReceivers(_iDs, _isQueue))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region SetSendMessageReceiver

public System.Data.SqlClient.SqlCommand GetCommand_SetSendMessageReceiver(Guid? _id, bool? _isSent, string _message)
{
return base.CreateCommand("msg.spSetSendMessageReceiver", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AIsSent", IsOutput = false, Value = _isSent == null ? DBNull.Value : (object)_isSent }, 
					new Parameter { Name = "@AMessage", IsOutput = false, Value = string.IsNullOrWhiteSpace(_message) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_message) }, 
	});

}

public async Task<ResultSet> SetSendMessageReceiverAsync(Guid? _id, bool? _isSent, string _message)
{
	using(var cmd = GetCommand_SetSendMessageReceiver(_id, _isSent, _message))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet SetSendMessageReceiver(Guid? _id, bool? _isSent, string _message)
{
	using(var cmd = GetCommand_SetSendMessageReceiver(_id, _isSent, _message))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddMessage

public System.Data.SqlClient.SqlCommand GetCommand_AddMessage(string _content, byte? _encodingType, long? _externalMessageID, Guid? _id, byte? _priority, string _receiverNumbers, DateTime? _sendDate, byte? _sendType, Guid? _sourceAccountID, short? _status, string _uDH)
{
return base.CreateCommand("msg.spAddMessage", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AContent", IsOutput = false, Value = string.IsNullOrWhiteSpace(_content) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_content) }, 
					new Parameter { Name = "@AEncodingType", IsOutput = false, Value = _encodingType == null ? DBNull.Value : (object)_encodingType }, 
					new Parameter { Name = "@AExternalMessageID", IsOutput = false, Value = _externalMessageID == null ? DBNull.Value : (object)_externalMessageID }, 
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@APriority", IsOutput = false, Value = _priority == null ? DBNull.Value : (object)_priority }, 
					new Parameter { Name = "@AReceiverNumbers", IsOutput = false, Value = string.IsNullOrWhiteSpace(_receiverNumbers) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_receiverNumbers) }, 
					new Parameter { Name = "@ASendDate", IsOutput = false, Value = _sendDate == null ? DBNull.Value : (object)_sendDate }, 
					new Parameter { Name = "@ASendType", IsOutput = false, Value = _sendType == null ? DBNull.Value : (object)_sendType }, 
					new Parameter { Name = "@ASourceAccountID", IsOutput = false, Value = _sourceAccountID == null ? DBNull.Value : (object)_sourceAccountID }, 
					new Parameter { Name = "@AStatus", IsOutput = false, Value = _status == null ? DBNull.Value : (object)_status }, 
					new Parameter { Name = "@AUDH", IsOutput = false, Value = string.IsNullOrWhiteSpace(_uDH) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_uDH) }, 
	});

}

public async Task<ResultSet> AddMessageAsync(string _content, byte? _encodingType, long? _externalMessageID, Guid? _id, byte? _priority, string _receiverNumbers, DateTime? _sendDate, byte? _sendType, Guid? _sourceAccountID, short? _status, string _uDH)
{
	using(var cmd = GetCommand_AddMessage(_content, _encodingType, _externalMessageID, _id, _priority, _receiverNumbers, _sendDate, _sendType, _sourceAccountID, _status, _uDH))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet AddMessage(string _content, byte? _encodingType, long? _externalMessageID, Guid? _id, byte? _priority, string _receiverNumbers, DateTime? _sendDate, byte? _sendType, Guid? _sourceAccountID, short? _status, string _uDH)
{
	using(var cmd = GetCommand_AddMessage(_content, _encodingType, _externalMessageID, _id, _priority, _receiverNumbers, _sendDate, _sendType, _sourceAccountID, _status, _uDH))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddMessages

public System.Data.SqlClient.SqlCommand GetCommand_AddMessages(string _messages)
{
return base.CreateCommand("msg.spAddMessages", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AMessages", IsOutput = false, Value = string.IsNullOrWhiteSpace(_messages) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_messages) }, 
	});

}

public async Task<ResultSet> AddMessagesAsync(string _messages)
{
	using(var cmd = GetCommand_AddMessages(_messages))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet AddMessages(string _messages)
{
	using(var cmd = GetCommand_AddMessages(_messages))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region DeleteMessage

public System.Data.SqlClient.SqlCommand GetCommand_DeleteMessage(string _iDS)
{
return base.CreateCommand("msg.spDeleteMessage", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AIDS", IsOutput = false, Value = string.IsNullOrWhiteSpace(_iDS) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_iDS) }, 
	});

}

public async Task<ResultSet> DeleteMessageAsync(string _iDS)
{
	using(var cmd = GetCommand_DeleteMessage(_iDS))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet DeleteMessage(string _iDS)
{
	using(var cmd = GetCommand_DeleteMessage(_iDS))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetMessages

public System.Data.SqlClient.SqlCommand GetCommand_GetMessages(string _receiverNumber, DateTime? _sendDateFrom, DateTime? _sendDateTo, Guid? _sourceAccountID, short? _status)
{
return base.CreateCommand("msg.spGetMessages", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AReceiverNumber", IsOutput = false, Value = string.IsNullOrWhiteSpace(_receiverNumber) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_receiverNumber) }, 
					new Parameter { Name = "@ASendDateFrom", IsOutput = false, Value = _sendDateFrom == null ? DBNull.Value : (object)_sendDateFrom }, 
					new Parameter { Name = "@ASendDateTo", IsOutput = false, Value = _sendDateTo == null ? DBNull.Value : (object)_sendDateTo }, 
					new Parameter { Name = "@ASourceAccountID", IsOutput = false, Value = _sourceAccountID == null ? DBNull.Value : (object)_sourceAccountID }, 
					new Parameter { Name = "@AStatus", IsOutput = false, Value = _status == null ? DBNull.Value : (object)_status }, 
	});

}

public async Task<ResultSet> GetMessagesAsync(string _receiverNumber, DateTime? _sendDateFrom, DateTime? _sendDateTo, Guid? _sourceAccountID, short? _status)
{
	using(var cmd = GetCommand_GetMessages(_receiverNumber, _sendDateFrom, _sendDateTo, _sourceAccountID, _status))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetMessages(string _receiverNumber, DateTime? _sendDateFrom, DateTime? _sendDateTo, Guid? _sourceAccountID, short? _status)
{
	using(var cmd = GetCommand_GetMessages(_receiverNumber, _sendDateFrom, _sendDateTo, _sourceAccountID, _status))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddTrySendMessage

public System.Data.SqlClient.SqlCommand GetCommand_AddTrySendMessage(string _message, Guid? _messageReceiverID)
{
return base.CreateCommand("msg.spAddTrySendMessage", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AMessage", IsOutput = false, Value = string.IsNullOrWhiteSpace(_message) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_message) }, 
					new Parameter { Name = "@AMessageReceiverID", IsOutput = false, Value = _messageReceiverID == null ? DBNull.Value : (object)_messageReceiverID }, 
	});

}

public async Task<ResultSet> AddTrySendMessageAsync(string _message, Guid? _messageReceiverID)
{
	using(var cmd = GetCommand_AddTrySendMessage(_message, _messageReceiverID))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet AddTrySendMessage(string _message, Guid? _messageReceiverID)
{
	using(var cmd = GetCommand_AddTrySendMessage(_message, _messageReceiverID))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetUnQueueReceiverMessages

public System.Data.SqlClient.SqlCommand GetCommand_GetUnQueueReceiverMessages()
{
return base.CreateCommand("msg.spGetUnQueueReceiverMessages", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

}

public async Task<ResultSet> GetUnQueueReceiverMessagesAsync()
{
	using(var cmd = GetCommand_GetUnQueueReceiverMessages())
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetUnQueueReceiverMessages()
{
	using(var cmd = GetCommand_GetUnQueueReceiverMessages())
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

}

class PBL: Database
{
#region Constructors
public PBL(string connectionString)
	:base(connectionString){}

public PBL(string connectionString, IModelValueBinder modelValueBinder)
	:base(connectionString, modelValueBinder){}
#endregion

#region GetAccount

public System.Data.SqlClient.SqlCommand GetCommand_GetAccount(Guid? _id)
{
return base.CreateCommand("pbl.spGetAccount", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
	});

}

public async Task<ResultSet> GetAccountAsync(Guid? _id)
{
	using(var cmd = GetCommand_GetAccount(_id))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetAccount(Guid? _id)
{
	using(var cmd = GetCommand_GetAccount(_id))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetAccountAdminNumbers

public System.Data.SqlClient.SqlCommand GetCommand_GetAccountAdminNumbers(Guid? _accountID)
{
return base.CreateCommand("pbl.spGetAccountAdminNumbers", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AAccountID", IsOutput = false, Value = _accountID == null ? DBNull.Value : (object)_accountID }, 
	});

}

public async Task<ResultSet> GetAccountAdminNumbersAsync(Guid? _accountID)
{
	using(var cmd = GetCommand_GetAccountAdminNumbers(_accountID))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetAccountAdminNumbers(Guid? _accountID)
{
	using(var cmd = GetCommand_GetAccountAdminNumbers(_accountID))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region DeleteAccount

public System.Data.SqlClient.SqlCommand GetCommand_DeleteAccount(Guid? _id)
{
return base.CreateCommand("pbl.spDeleteAccount", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
	});

}

public async Task<ResultSet> DeleteAccountAsync(Guid? _id)
{
	using(var cmd = GetCommand_DeleteAccount(_id))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet DeleteAccount(Guid? _id)
{
	using(var cmd = GetCommand_DeleteAccount(_id))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetAccounts

public System.Data.SqlClient.SqlCommand GetCommand_GetAccounts(bool? _enabled, Guid? _id)
{
return base.CreateCommand("pbl.spGetAccounts", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AEnabled", IsOutput = false, Value = _enabled == null ? DBNull.Value : (object)_enabled }, 
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
	});

}

public async Task<ResultSet> GetAccountsAsync(bool? _enabled, Guid? _id)
{
	using(var cmd = GetCommand_GetAccounts(_enabled, _id))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetAccounts(bool? _enabled, Guid? _id)
{
	using(var cmd = GetCommand_GetAccounts(_enabled, _id))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region ModifyAccount

public System.Data.SqlClient.SqlCommand GetCommand_ModifyAccount(decimal? _alertCreditAmount, string _domain, bool? _enabled, Guid? _id, bool? _isNewRecord, string _number, string _password, string _title, string _userName)
{
return base.CreateCommand("pbl.spModifyAccount", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AAlertCreditAmount", IsOutput = false, Value = _alertCreditAmount == null ? DBNull.Value : (object)_alertCreditAmount }, 
					new Parameter { Name = "@ADomain", IsOutput = false, Value = string.IsNullOrWhiteSpace(_domain) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_domain) }, 
					new Parameter { Name = "@AEnabled", IsOutput = false, Value = _enabled == null ? DBNull.Value : (object)_enabled }, 
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AIsNewRecord", IsOutput = false, Value = _isNewRecord == null ? DBNull.Value : (object)_isNewRecord }, 
					new Parameter { Name = "@ANumber", IsOutput = false, Value = string.IsNullOrWhiteSpace(_number) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_number) }, 
					new Parameter { Name = "@APassword", IsOutput = false, Value = string.IsNullOrWhiteSpace(_password) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_password) }, 
					new Parameter { Name = "@ATitle", IsOutput = false, Value = string.IsNullOrWhiteSpace(_title) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_title) }, 
					new Parameter { Name = "@AUserName", IsOutput = false, Value = string.IsNullOrWhiteSpace(_userName) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_userName) }, 
	});

}

public async Task<ResultSet> ModifyAccountAsync(decimal? _alertCreditAmount, string _domain, bool? _enabled, Guid? _id, bool? _isNewRecord, string _number, string _password, string _title, string _userName)
{
	using(var cmd = GetCommand_ModifyAccount(_alertCreditAmount, _domain, _enabled, _id, _isNewRecord, _number, _password, _title, _userName))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet ModifyAccount(decimal? _alertCreditAmount, string _domain, bool? _enabled, Guid? _id, bool? _isNewRecord, string _number, string _password, string _title, string _userName)
{
	using(var cmd = GetCommand_ModifyAccount(_alertCreditAmount, _domain, _enabled, _id, _isNewRecord, _number, _password, _title, _userName))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region DeleteConfig

public System.Data.SqlClient.SqlCommand GetCommand_DeleteConfig(Guid? _id, string _iDs)
{
return base.CreateCommand("pbl.spDeleteConfig", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AIDs", IsOutput = false, Value = string.IsNullOrWhiteSpace(_iDs) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_iDs) }, 
	});

}

public async Task<ResultSet> DeleteConfigAsync(Guid? _id, string _iDs)
{
	using(var cmd = GetCommand_DeleteConfig(_id, _iDs))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet DeleteConfig(Guid? _id, string _iDs)
{
	using(var cmd = GetCommand_DeleteConfig(_id, _iDs))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetConfig

public System.Data.SqlClient.SqlCommand GetCommand_GetConfig(string _name)
{
return base.CreateCommand("pbl.spGetConfig", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AName", IsOutput = false, Value = string.IsNullOrWhiteSpace(_name) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_name) }, 
	});

}

public async Task<ResultSet> GetConfigAsync(string _name)
{
	using(var cmd = GetCommand_GetConfig(_name))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetConfig(string _name)
{
	using(var cmd = GetCommand_GetConfig(_name))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region SetConfig

public System.Data.SqlClient.SqlCommand GetCommand_SetConfig(string _data)
{
return base.CreateCommand("pbl.spSetConfig", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AData", IsOutput = false, Value = string.IsNullOrWhiteSpace(_data) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_data) }, 
	});

}

public async Task<ResultSet> SetConfigAsync(string _data)
{
	using(var cmd = GetCommand_SetConfig(_data))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet SetConfig(string _data)
{
	using(var cmd = GetCommand_SetConfig(_data))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

}

}
