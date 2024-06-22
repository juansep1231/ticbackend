namespace backendfepon.Models
{
    public class Responsible
    {
        public int Responsible_Id { get; set; }
        public int AdministrativeMember_Id { get; set; }
        public int Event_Id { get; set; }
        public string Responsible_Name { get; set; }
        public string Event_Role { get; set; }

     
        
    }
}
