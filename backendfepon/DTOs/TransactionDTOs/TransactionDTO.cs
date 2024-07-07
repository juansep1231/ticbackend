namespace backendfepon.DTOs.TransactionDTOs
{
    public class TransactionDTO
    {
        public int Transaction_Id { get; set; }
        public string Transaction_Type_Name { get; set; }
        public DateTime Date { get; set; }
        public string Origin_Account_Name { get; set; }
        public string Destination_Account_Name { get; set; }
        public decimal Value { get; set; }
        public string Reason { get; set; }
    }
}
