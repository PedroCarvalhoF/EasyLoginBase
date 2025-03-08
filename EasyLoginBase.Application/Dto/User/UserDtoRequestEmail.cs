using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Application.Dto.User;

public class UserDtoRequestEmail
{
    [Required]
    [EmailAddress]
    public string Email { get; private set; }

    public UserDtoRequestEmail(string email)
    {
        Email = email;
    }
}
