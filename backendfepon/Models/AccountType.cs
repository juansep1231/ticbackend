namespace backendfepon.Models
{
    public class AccountType
    {
        public int Account_Type_Id { get; set; }
        public int Account_Id { get; set; }
        public string Account_Type_Name { get; set; }

        public ICollection<AccountingAccount> AccountingAccounts{ get; set; }
    }
}
