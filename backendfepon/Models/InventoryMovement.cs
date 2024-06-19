namespace backendfepon.Models
{
    public class InventoryMovement
    {
        public int MovementId { get; set; }
        public int TransactionId { get; set; }
        public int InventoryMovementId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}