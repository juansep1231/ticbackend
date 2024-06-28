namespace backendfepon.DTOs.ProductDTOs
{
    public class CreateUpdateProductDTO
    {
        public int State_Id { get; set; }

        public int Provider_Id { get; set; }
        public int Category_Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal? Economic_Value { get; set; }

        public int Quantity { get; set; }

        public string Label { get; set; } = null!;
    }
}
