using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds = SmsService.Core.DataSource;
using @Model = SmsService.Core.Model;

namespace SmsService.Infrastructure.DAL
{
	class AccountDataSource : DataSource, ds.IAccountDataSource
	{
		public AccountDataSource(AppCore.IOC.IContainer container)
			: base(container)
		{
		}

        private async Task<AppCore.Result> ModifyAsync(bool isNewRecord, Model.Account model)
        {
            try
            {
                return (await _dbPublic.ModifyAccountAsync(
                    _isNewRecord: isNewRecord, 
                    _id: model.ID
                    , _title: model.Title
                    , _domain: model.Domain
                    , _userName: model.UserName
                    , _password: model.Password
                    , _number: model.Number
                    , _enabled: model.Enabled
                    , _alertCreditAmount: model.AlertCreditAmount))
                        .ToActionResult();
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error on {(isNewRecord ? "create new " : "update")} account.");
                return AppCore.Result.Failure();
            }
        }

        public Task<AppCore.Result> CreateAsync(Model.Account model)
            => ModifyAsync(true, model);

        public Task<AppCore.Result> UpdateAsync(Model.Account model)
            => ModifyAsync(false, model);

        public async Task<AppCore.Result> DeleteAsync(Guid id)
        {
            try
            {
                return (await _dbPublic.DeleteAccountAsync(_id: id))
                       .ToActionResult();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on delete account.");
                return AppCore.Result.Failure();
            }
        }

        private async Task<AppCore.Result<IEnumerable<Model.Account>>> listAsync(bool? enabled)
        {
            try
            {
                return (await _dbPublic.GetAccountsAsync(_id: null, _enabled: enabled))
                        .ToListActionResult<Model.Account>();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on retrieve list of accounts.");
                return AppCore.Result<IEnumerable<Model.Account>>.Failure();
            }
        }

        public async Task<AppCore.Result<IEnumerable<Model.AccountAdminNumber>>> ListAdminNumbersAsync(Guid accountID)
        {
            try
            {
                return (await _dbPublic.GetAccountAdminNumbersAsync(_accountID: accountID))
                        .ToListActionResult<Model.AccountAdminNumber>();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on retrieve list of accounts.");
                return AppCore.Result<IEnumerable<Model.AccountAdminNumber>>.Failure();
            }
        }

        public Task<AppCore.Result<IEnumerable<Model.Account>>> ListAsync()
            => listAsync(null);

        public Task<AppCore.Result<IEnumerable<Model.Account>>> ActivesAsync()
            => listAsync(true);

        public Task<AppCore.Result<IEnumerable<Model.Account>>> InactivesAsync()
            => listAsync(false);

        public async Task<AppCore.Result<Model.Account>> GetAsync(Guid id)
        {
            try
            {
                var result = (await _dbPublic.GetAccountAsync(_id: id))
                        .ToActionResult<Model.Account>();

                var adminNumbers = await ListAdminNumbersAsync(accountID: result.Data.ID);
                if (!adminNumbers.Success)
                    return AppCore.Result<Model.Account>.Failure(message: adminNumbers.Message);

                result.Data.AdminNumbers = adminNumbers.Data.ToList();
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on get account by id.");
                return AppCore.Result<Model.Account>.Failure();
            }
        }
    }
}
;