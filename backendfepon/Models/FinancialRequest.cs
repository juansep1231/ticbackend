namespace backendfepon.Models
{
    public class FinancialRequest
    {
        public int RequestId { get; set; }
        public int AdministrativeMemberId { get; set; }
        public int RequestStatusId { get; set; }
        public decimal Value { get; set; }
        public string Reason { get; set; }
        public string State { get; set; }
    }
}
