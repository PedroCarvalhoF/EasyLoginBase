using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Services.Tools.UseCase;

public partial class DtoMapper
{
    public static UserDto ParceUserDto(UserEntity user)
    {
        return new UserDto(user.Id, user.Nome!, user.SobreNome!, user.Email!);
    }

    public static IEnumerable<UserDto> ParceUsersDtos(IEnumerable<UserEntity> users)
    {
        foreach (var user in users)
        {
            yield return ParceUserDto(user);
        }
    }
}
