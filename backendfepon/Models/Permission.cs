namespace backendfepon.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public int EventId { get; set; }
        public string Request { get; set; }
        public string RequestStatus { get; set; }
    }
}
