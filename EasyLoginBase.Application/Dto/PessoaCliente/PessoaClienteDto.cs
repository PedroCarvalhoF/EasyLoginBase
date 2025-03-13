namespace EasyLoginBase.Application.Dto.PessoaCliente;

public class PessoaClienteDto
{
    public Guid Id { get; set; }
    public Guid UsuarioEntityClienteId { get; set; }
    public string? NomeUsuarioCliente { get; set; }
    public string? NomeFantasia { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataVencimentoUso { get; set; }

    public PessoaClienteDto(Guid id, Guid usuarioEntityClienteId, string? nomeUsuarioCliente, string? nomeFantasia, DateTime dataAbertura, DateTime dataVencimentoUso)
    {
        Id = id;
        UsuarioEntityClienteId = usuarioEntityClienteId;
        NomeUsuarioCliente = nomeUsuarioCliente;
        NomeFantasia = nomeFantasia;
        DataAbertura = dataAbertura;
        DataVencimentoUso = dataVencimentoUso;
    }
}
