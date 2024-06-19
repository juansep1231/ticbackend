namespace backendfepon.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int AdministrativeMemberId { get; set; }
        public int StateId { get; set; }
        public string Password { get; set; }
    }
}

