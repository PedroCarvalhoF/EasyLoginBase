namespace EasyLoginBase.Application.Dto.Filial;

public class FiliaDtoCreateRequest
{
    public string NomeFilial { get; private set; }

    public FiliaDtoCreateRequest(string nomeFilial)
    {
        NomeFilial = nomeFilial;
    }
}
