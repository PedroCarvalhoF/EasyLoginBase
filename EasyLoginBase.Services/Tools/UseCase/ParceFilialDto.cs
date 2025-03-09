using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Domain.Entities.Filial;

namespace EasyLoginBase.Services.Tools.UseCase;

public partial class DtoMapper
{
    public static FilialDto ParceFilial(FilialEntity user)
    {
        return new FilialDto(user.IdFilial, user.NomeFilial);
    }
}
