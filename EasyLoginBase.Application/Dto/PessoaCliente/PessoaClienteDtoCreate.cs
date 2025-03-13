namespace EasyLoginBase.Application.Dto.PessoaCliente;

public class PessoaClienteDtoCreate
{
    public Guid UsuarioEntityClienteId { get; private set; }
    public string NomeFantasia { get; private set; }
    public PessoaClienteDtoCreate(Guid usuarioEntityClienteId, string nomeFantasia)
    {
        UsuarioEntityClienteId = usuarioEntityClienteId;
        NomeFantasia = nomeFantasia;
    }
}
