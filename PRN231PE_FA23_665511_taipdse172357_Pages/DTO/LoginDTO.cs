using System.ComponentModel.DataAnnotations;

namespace PRN231PE_FA23_665511_taipdse172357_Pages.DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email Is not empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is not empty")]
        public string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string AccountId { get; set; }
    }
}
