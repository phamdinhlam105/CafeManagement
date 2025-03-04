using CafeManagement.Enums;

namespace CafeManagement.Dtos.Request
{
    public class SetRoleRequest
    {
        public string UserId {  get; set; }
        public UserRole Role { get; set; }
    }
}
