namespace EasyLoginBase.Application.Dto.Filial;

public class FilialDtoUpdateRequest
{
    public Guid IdFilial { get; private set; }
    public string NomeFilial { get; set; }
    public FilialDtoUpdateRequest(Guid idFilial, string nomeFilial)
    {
        IdFilial = idFilial;
        NomeFilial = nomeFilial;
    }
}
