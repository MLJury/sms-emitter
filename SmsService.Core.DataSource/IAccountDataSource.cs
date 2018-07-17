using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsService.Core.DataSource
{
	public interface IAccountDataSource : IDataSource
	{
        Task<AppCore.Result> CreateAsync(Model.Account model);

        Task<AppCore.Result> UpdateAsync(Model.Account model);

        Task<AppCore.Result> DeleteAsync(Guid id);

        Task<AppCore.Result<IEnumerable<Model.Account>>> ListAsync();

        Task<AppCore.Result<IEnumerable<Model.Account>>> ActivesAsync();

        Task<AppCore.Result<IEnumerable<Model.Account>>> InactivesAsync();

        Task<AppCore.Result<Model.Account>> GetAsync(Guid id);

        Task<AppCore.Result<IEnumerable<Model.AccountAdminNumber>>> ListAdminNumbersAsync(Guid accountID);
    }
}
