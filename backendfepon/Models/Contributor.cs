namespace backendfepon.Models
{
    public class Contributor
    {
        public int Contributor_Id { get; set; }
        public int Plan_Id { get; set; }
        public int Transaction_Id { get; set; }
        public int Student_Id { get; set; }

        public Student Student { get; set; }

        public ContributionPlan ContributionPlan { get; set; }

        public Transaction Transaction { get; set; }



    }
}
