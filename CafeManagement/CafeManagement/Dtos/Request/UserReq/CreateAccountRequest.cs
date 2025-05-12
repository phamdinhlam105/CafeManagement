using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Dtos.Request.UserReq
{
    public class CreateAccountRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
