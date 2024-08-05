namespace CustomerServiceWebApi.Application.DTOs
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
