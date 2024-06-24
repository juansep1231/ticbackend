namespace backendfepon.DTOs.TransactionDTOs
{
    public class CreateUpdateTransactionDTO
    {
        public DateTime Date { get; set; }
        public int Origin_Account { get; set; }
        public int Destination_Account { get; set; }
        public decimal Value { get; set; }
        public string Reason { get; set; }
    }
}
