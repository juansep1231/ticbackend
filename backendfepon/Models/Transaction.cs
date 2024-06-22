namespace backendfepon.Models
{
    public class Transaction
    {
      
            public int Transaction_Id { get; set; }
            public int Account_Id { get; set; }
            public int? Contributor_Id { get; set; }
            public DateTime Date { get; set; }
            public int Origin_Account { get; set; }
            public int Destination_Account { get; set; }
            public decimal Value { get; set; }
            public string Reason { get; set; }

            // Navigation properties
            public Contributor? Contributor { get; set; }
            
            public EventIncome EventIncome { get; set; }
        
        
    }
}
