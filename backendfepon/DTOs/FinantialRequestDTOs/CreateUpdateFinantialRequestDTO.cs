namespace backendfepon.DTOs.FinantialRequestDTOs
{
    public class CreateUpdateFinantialRequestDTO
    {
        public int AdministrativeMember_Id { get; set; }
        public int Request_Status_Id { get; set; }
        public decimal Value { get; set; }
        public string Reason { get; set; }
    }
}
