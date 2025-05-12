using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Dtos.Request
{
    public class CustomerRequest
    {
        [Required]
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
