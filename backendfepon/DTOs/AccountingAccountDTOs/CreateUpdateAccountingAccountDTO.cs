namespace backendfepon.DTOs.AccountingAccountDTOs
{
    public class CreateUpdateAccountingAccountDTO
    {
        public int Account_Type_Id { get; set; }
        public string Account_Name { get; set; }
        public decimal Current_Value { get; set; }
        public DateTime Initial_Balance_Date { get; set; }
        public decimal Initial_Balance { get; set; }
        public string Accounting_Account_Status { get; set; }
    }
}
