using System;
using System.Collections.Concurrent;

namespace SmsService.Core.Model
{
		public enum SmsServiceAccounts : byte
		{
				Unknown = 0,
					Daadkhahi = 1,
					Azmoon = 2,
					Aro = 3,
			}
	    public class SmsServiceDic
		{
		readonly static Lazy<SmsServiceDic> _instance = new Lazy<SmsServiceDic>(() => new SmsServiceDic());

		        public static SmsServiceDic Instance
             => _instance.Value;

				readonly ConcurrentDictionary<SmsServiceAccounts, Guid> _items = new ConcurrentDictionary<SmsServiceAccounts, Guid>()
		{
			[SmsServiceAccounts.Unknown] = Guid.Empty,
					[SmsServiceAccounts.Daadkhahi] = new Guid("18dead81-a382-42c0-9cfd-3efe8863de56"),
					[SmsServiceAccounts.Azmoon] = new Guid("d7480438-e252-43b6-9805-c6dd59c30679"),
					[SmsServiceAccounts.Aro] = new Guid("f82035b4-570a-4ed9-af72-d0cc82512eb4"),
		};

		public Guid this[SmsServiceAccounts account]
        {
            get
            {
                Guid accountId = Guid.Empty;
                _items.TryGetValue(account, out accountId);
                return accountId;
            }
        }
		}
}
