namespace backendfepon.Models
{
    public class Association
    {
        public int AssociationId { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string Objective { get; set; }
        public string Logo { get; set; } // Si el campo LOGO es de tipo imagen
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
