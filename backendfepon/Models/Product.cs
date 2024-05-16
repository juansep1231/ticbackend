namespace backendfepon.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal EconomicValue { get; set; }
        public int Quantity { get; set; }
        public string Label { get; set; }
    }
}
