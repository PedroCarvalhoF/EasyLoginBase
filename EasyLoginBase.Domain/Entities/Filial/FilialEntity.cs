namespace EasyLoginBase.Domain.Entities.Filial;

public class FilialEntity
{
    public Guid IdFilial { get; private set; }
    public string NomeFilial { get; set; }
    public FilialEntity(Guid idFilial, string nomeFilial)
    {
        IdFilial = idFilial;
        NomeFilial = nomeFilial;
    }

    public static FilialEntity Create(string nomeFilial)
    => new FilialEntity(Guid.NewGuid(), nomeFilial);
}
