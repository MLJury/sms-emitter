using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ds = Kama.SmsService.Core.DataSource;
using svc = Kama.SmsService.Core.Service;
using @Model = Kama.SmsService.Core.Model;

namespace Kama.SmsService.Domain
{
	class AccountService : Service<ds.IAccountDataSource>, svc.IAccountService
	{
		public AccountService(AppCore.IOC.IContainer container, ds.IAccountDataSource dataSource)
			: base(container, dataSource)
		{
		}

        public Task<AppCore.Result> CreateAsync(Core.IRequestInfo request, Model.Account model)
        {
            //model.ID = Guid.NewGuid();
            return _dataSource.CreateAsync(model);
        }

        public Task<AppCore.Result> UpdateAsync(Core.IRequestInfo request, Model.Account model)
        {
            if (model.ID.IsNullOrEmpty())
                return AppCore.Result.FailureAsync();

            return _dataSource.UpdateAsync(model);
        }

        public Task<AppCore.Result> DeleteAsync(Core.IRequestInfo request, Model.Account model)
        {
            if (model.ID.IsNullOrEmpty())
                return AppCore.Result.FailureAsync();

            return _dataSource.DeleteAsync(model.ID);
        }

        public Task<AppCore.Result<IEnumerable<Model.Account>>> ListAsync(Core.IRequestInfo request)
            => _dataSource.ListAsync();

        public Task<AppCore.Result<Model.Account>> GetAsync(Core.IRequestInfo request, Guid id)
            => _dataSource.GetAsync(id);
    }
}
