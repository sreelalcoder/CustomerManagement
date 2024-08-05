using CustomerServiceWebClient.Validators;
using System.ComponentModel.DataAnnotations;

namespace CustomerServiceWebClient.Models.DTOs
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [ValidPhoneNumberAttribute]
        public string PhoneNumber { get; set; }
    }
}
