namespace backendfepon.Models
{
    public class State
    {
        public int State_Id { get; set; }
        public string State_Name { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }
        public ICollection<ContributionPlan> ContributionPlans { get; set; }
        public ICollection<User> Users { get; set; }

        public ICollection<Event> Events { get; set; }
        public ICollection<Association> Associations { get; set; }

    }
}
