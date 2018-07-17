using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsService.Core.Service
{
	public interface IAccountService : IService
	{
        Task<AppCore.Result> CreateAsync(IRequestInfo request, Model.Account model);

        Task<AppCore.Result> UpdateAsync(IRequestInfo request, Model.Account model);

        Task<AppCore.Result> DeleteAsync(IRequestInfo request, Model.Account model);

        Task<AppCore.Result<IEnumerable<Model.Account>>> ListAsync(IRequestInfo request);

        Task<AppCore.Result<Model.Account>> GetAsync(IRequestInfo request, Guid id);
    }
}
