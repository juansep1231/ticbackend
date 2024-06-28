namespace backendfepon.DTOs.InventoryMovementDTOs
{
    public class InventoryMovementDTO
    {
        public int Movement_Id { get; set; }
        public int Transaction_Id { get; set; }

        public string Product_Name { get; set; }
        public string Inventory_Movement_Name { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
