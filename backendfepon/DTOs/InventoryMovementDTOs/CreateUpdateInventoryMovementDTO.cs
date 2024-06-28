namespace backendfepon.DTOs.InventoryMovementDTOs
{
    public class CreateUpdateInventoryMovementDTO
    {
        public int Transaction_Id { get; set; }
        public int Inventory_Movement_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
