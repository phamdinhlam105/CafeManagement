﻿namespace CafeManagement.Dtos.Request.UserReq
{
    public class ChangePasswordRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
