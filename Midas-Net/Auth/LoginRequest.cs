using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Midas.Net.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
