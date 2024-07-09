﻿namespace backendfepon.Models
{
    public class Contributor
    {
        public int Contributor_Id { get; set; }
        public int State_Id { get; set; }
        public int Plan_Id { get; set; }
        public int Faculty_Id { get; set; }
        public int Career_Id { get; set; }
        public int Transaction_Id { get; set; }
        public DateTime Contributor_Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //public int Student_Id { get; set; }

        //public Student Student { get; set; }

        public Career Career { get; set; }
        public Faculty Faculty { get; set; }

        public ContributionPlan ContributionPlan { get; set; }

        public Transaction Transaction { get; set; }
        public State State { get; set; }



    }
}
