namespace backendfepon.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int StateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; }
        public string BudgetStatus { get; set; }
    }
}
