namespace backendfepon.DTOs.ContributionPlanDTOs
{
    public class CreateUpdateContributionPlanDTO
    {
        public int Academic_Period_Id { get; set; }
        public int State_Id { get; set; }
        public string Name { get; set; }
        public string Benefits { get; set; }
        public decimal Economic_Value { get; set; }
    }
}
