namespace backendfepon.DTOs.EventDTOs
{
    public class CreateUpdateEventDTO
    {
        public int State_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; }
        public string Budget_Status { get; set; }
        public string Event_Location { get; set; }
        public string Hiring { get; set; }
    }
}
