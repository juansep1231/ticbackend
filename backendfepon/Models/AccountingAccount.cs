namespace backendfepon.Models
{
    public class AccountingAccount
    {
        public int AccountId { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountName { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime InitialBalanceDate { get; set; }
        public decimal InitialBalance { get; set; }
        public string AccountingAccountStatus { get; set; }
    }
}
