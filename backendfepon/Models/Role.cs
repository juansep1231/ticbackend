namespace backendfepon.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public int AdministrativeMemberId { get; set; }
        public string RoleName { get; set; }
    }
}
