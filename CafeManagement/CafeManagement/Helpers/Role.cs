using CafeManagement.Enums;

namespace CafeManagement.Helpers
{
    public static class Role
    {
        public const string Admin = "administrator";
        public const string Manager = "Quản lý";
        public const string Employee = "Nhân viên";
        public const string Customer = "Khách";
        public static string GetRoleName(UserRole role)
        {
            return role switch
            {
                UserRole.Admin => Role.Admin,
                UserRole.Manager => Role.Manager,
                UserRole.Employee => Role.Employee,
                UserRole.Customer => Role.Customer,
                _ => throw new ArgumentException("Vai trò không hợp lệ", nameof(role))
            };
        }

    }
}
