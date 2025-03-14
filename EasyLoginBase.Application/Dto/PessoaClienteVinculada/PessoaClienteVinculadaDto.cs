namespace EasyLoginBase.Application.Dto.PessoaClienteVinculada;

public class PessoaClienteVinculadaDto
{
    public Guid PessoaClienteEntityId { get; set; }
    public Guid UsuarioEntityClienteId { get; set; }
    public string? NomeFantasia { get; set; }
    public Guid UsuarioVinculadoId { get; set; }
    public string? NomeUsuarioVinculado { get; set; }
    public bool AcessoPermitido { get; set; }
    public PessoaClienteVinculadaDto() { }
}
