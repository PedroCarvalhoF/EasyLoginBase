namespace EasyLoginBase.Application.Dto.Filial;

public class FilialDto
{
    public Guid Id { get;  set; }
    public string NomeFilial { get; set; }
    public FilialDto(Guid id, string nomeFilial)
    {
        Id = id;
        NomeFilial = nomeFilial;
    }
}
