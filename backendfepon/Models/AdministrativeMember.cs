namespace backendfepon.Models
{
    public class AdministrativeMember
    {
        public int Administrative_Member_Id { get; set; }
        public int Student_Id { get; set; }
        public int Role_Id { get; set; } 
        public int State_Id { get; set; } 

        public Student Student { get; set; }

        public Responsible Responsible { get; set; }

        public Role Role { get; set; }
        public State State { get; set; }

        public ICollection<FinancialRequest> FinancialRequests { get; set; }
    }
}
