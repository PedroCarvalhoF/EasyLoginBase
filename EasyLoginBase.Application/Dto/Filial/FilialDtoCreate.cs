namespace EasyLoginBase.Application.Dto.Filial;

public class FilialDtoCreate
{
    public string NomeFilial { get; private set; }
    public FilialDtoCreate(string nomeFilial)
    {
        NomeFilial = nomeFilial;
    }
}
