using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Dtos.Request
{
    public class CustomerRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
