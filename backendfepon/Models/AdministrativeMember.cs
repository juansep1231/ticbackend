namespace backendfepon.Models
{
    public class AdministrativeMember
    {
        public int Administrative_Member_Id { get; set; }
        public int User_Id { get; set; }
        public int Student_Id { get; set; }
        public string Photo { get; set; } // Cambiar el tipo de dato en el caso de que este no sea un string

        public User User { get; set; }
        public Student Student { get; set; }

        public Responsible Responsible { get; set; }

        public ICollection<FinancialRequest> FinancialRequests { get; set; }
    }
}
