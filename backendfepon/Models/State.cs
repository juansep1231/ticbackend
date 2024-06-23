namespace backendfepon.Models
{
    public class State
    {
        public int State_Id { get; set; }
        public string State_Name { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}
