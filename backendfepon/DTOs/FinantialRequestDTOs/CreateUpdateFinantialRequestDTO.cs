namespace backendfepon.DTOs.FinantialRequestDTOs
{
    public class CreateUpdateFinantialRequestDTO
    {
        public int AdministrativeMember_Name { get; set; }
        public int Request_Status_Name { get; set; }
        public decimal Value { get; set; }
        public string Reason { get; set; }
    }
}
