namespace EasyLoginBase.Application.Dto.Filial;

public class FilialDtoCreate
{
    public string NomeFilial { get; private set; }
    public Guid PessoaClienteId { get; private set; }

    public FilialDtoCreate(string nomeFilial, Guid pessoaClienteId)
    {
        NomeFilial = nomeFilial;
        PessoaClienteId = pessoaClienteId;
    }
}
