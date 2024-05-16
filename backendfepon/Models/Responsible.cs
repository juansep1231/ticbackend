namespace backendfepon.Models
{
    public class Responsible
    {
        public int ResponsibleId { get; set; }
        public int AdministrativeMemberId { get; set; }
        public int EventId { get; set; }
        public string ResponsibleName { get; set; }
        public string EventRole { get; set; }
    }
}
