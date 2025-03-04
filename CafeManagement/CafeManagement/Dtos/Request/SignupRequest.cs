namespace CafeManagement.Dtos.Request
{
    public class SignupRequest
    {
        public string UserName {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name {  get; set; }
        public DateOnly? BirthDay {  get; set; }
        public string PhoneNumber {  get; set; }

    }
}
