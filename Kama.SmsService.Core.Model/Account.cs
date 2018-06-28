
using System;
using System.Collections.Generic;

namespace Kama.SmsService.Core.Model
{
    public class Account: Model
    {
        public string Title { get; set; }

        public string Domain { get; set; }

        public string Number { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Enabled { get; set; }

        public decimal AlertCreditAmount { get; set; }

        public int CreditAlertCount { get; set; }

        public List<AccountAdminNumber> AdminNumbers { get; set; }

        public override string ToString()
            => Title;
    }
    public class AccountAdminNumber : Model
    {
        public Guid AccountID { get; set; }
        public string Number { get; set; }
    }
}
