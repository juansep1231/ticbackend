namespace backendfepon.DTOs.ResponsibleDTOs
{
    public class CreateUpdateResponsibleDTO
    {
        public int Responsible_Id { get; set; }
        public int AdministrativeMember_Id { get; set; }
        public int Event_Id { get; set; }
        public string Event_Role { get; set; }
    }
}
