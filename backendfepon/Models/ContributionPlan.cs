namespace backendfepon.Models
{
    public class ContributionPlan
    {
        public int PlanId { get; set; }
        public int AcademicPeriodId { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public string Benefits { get; set; }
        public decimal EconomicValue { get; set; }
        public string AcademicPeriod { get; set; }
    }
}
