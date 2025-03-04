using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Models
{
    public class Profile
    {
        public Guid Id {  get; set; }
        public string UserId {  get; set; }
        public string Name {  get; set; }
        public int? Age {  get; set; }
        public DateOnly? BirthDay { get; set; }
        [EmailAddress]
        public string Email {  get; set; }
        public string PhoneNumber {  get; set; }
        public string? PictureURL {  get; set; }
        public DateTime joinDate {  get; set; }
    }
}
