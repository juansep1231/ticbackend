namespace backendfepon.Models
{
    public class EventExpense
    {
        public int ExpenseId { get; set; }
        public int TransactionId { get; set; }
        public int EventId { get; set; }
        public int ProviderId { get; set; }
        public int ResponsibleId { get; set; }
    }
}
