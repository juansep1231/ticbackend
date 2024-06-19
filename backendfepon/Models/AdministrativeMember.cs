namespace backendfepon.Models
{
    public class AdministrativeMember
    {
        public int AdministrativeMemberId { get; set; }
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public int ResponsibleId { get; set; }
        public string Photo { get; set; } // Cambiar el tipo de dato en el caso de que este no sea un string
    }
}
