namespace backendfepon.Models
{
    public class User
    {
        public int User_Id { get; set; }
        public int State_Id { get; set; }
        public AdministrativeMember AdministrativeMember { get; set; }

        public State State { get; set; }
    }
}

