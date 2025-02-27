using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Models
{
    public class User:IdentityUser
    {
        public Profile Profile { get; set; }
    }
}
