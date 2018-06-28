using System;
using System.Threading.Tasks;
using ds = Kama.SmsService.Core.DataSource;
using @Model = Kama.SmsService.Core.Model;

namespace Kama.SmsService.Infrastructure.DAL
{
    class ConfigDataSource : DataSource, ds.IConfigDataSource
    {
        public ConfigDataSource(AppCore.IOC.IContainer container)
            : base(container)
        {
        }

        const string Key_PrioritySendCount = "PrioritySendCount";

        private T Deserialize<T>(Model.Config config)
        {
            try
            {
                return _objSerializer.Deserialize<T>(config?.Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> SaveAsync(Model.Config[] configs)
        {
            try
            {
                return (await _dbPublic.SetConfigAsync(_objSerializer.Serialize(configs)))
                        .ToActionResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result<Model.Config>> GetAsync(string name)
        {
            try
            {
                var result = await _dbPublic.GetConfigAsync(name);
                return (await _dbPublic.GetConfigAsync(name)).ToActionResult<Model.Config>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Task<AppCore.Result> SetPrioritySendCountAsync(Model.PrioritySendCount model)
            => this.SaveAsync(new Model.Config[] { new Model.Config { Name = Key_PrioritySendCount, Value = _objSerializer.Serialize(model) } });

        public async Task<AppCore.Result<Model.PrioritySendCount>> GetPrioritySendCountAsync()
        {
            var result = await this.GetAsync(Key_PrioritySendCount);

            if (result.Success)
                return AppCore.Result<Model.PrioritySendCount>.Successful(data: Deserialize<Model.PrioritySendCount>(result.Data));
            else
                return AppCore.Result<Model.PrioritySendCount>.Failure(code: result.Code);
        }

    }
}
