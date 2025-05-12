using CafeManagement.Enums;

namespace CafeManagement.Dtos.Request.UserReq
{
    public class SetRoleRequest
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
