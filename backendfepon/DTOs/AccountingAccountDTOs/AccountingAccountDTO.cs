namespace backendfepon.DTOs.AccountingAccountDTOs
{
    public class AccountingAccountDTO
    {
        public int Account_Id { get; set; }
        public string Account_Type_Name { get; set; }
        public string Account_Name { get; set; }
        public decimal Current_Value { get; set; }
        public DateTime Initial_Balance_Date { get; set; }
        public decimal Initial_Balance { get; set; }
        public string Accounting_Account_Status { get; set; }
    }
}
