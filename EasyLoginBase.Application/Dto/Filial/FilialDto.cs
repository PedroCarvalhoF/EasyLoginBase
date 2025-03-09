namespace EasyLoginBase.Application.Dto.Filial;

public class FilialDto
{
    public Guid IdFilial { get; private set; }
    public string NomeFilial { get; set; }
    public FilialDto(Guid idFilial, string nomeFilial)
    {
        IdFilial = idFilial;
        NomeFilial = nomeFilial;
    }
}
