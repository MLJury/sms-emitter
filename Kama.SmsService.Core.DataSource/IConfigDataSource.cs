using System.Threading.Tasks;

namespace Kama.SmsService.Core.DataSource
{
	public interface IConfigDataSource : IDataSource
	{
        Task<AppCore.Result> SaveAsync(Model.Config[] configs);

        Task<AppCore.Result<Model.Config>> GetAsync(string name);

        Task<AppCore.Result> SetPrioritySendCountAsync(Model.PrioritySendCount model);

        Task<AppCore.Result<Model.PrioritySendCount>> GetPrioritySendCountAsync();
    }
}
