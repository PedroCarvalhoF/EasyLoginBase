using EasyLoginBase.Application.Dto.User;

namespace EasyLoginBase.Application.Dto.PessoaCliente;

public class PessoaClienteDto
{
    public Guid Id { get; set; }
    public Guid UsuarioEntityClienteId { get; set; }
    public string? NomeUsuarioCliente { get; set; }
    public string? NomeFantasia { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataVencimentoUso { get; set; }
    public List<UserDto>? UsuariosVinculadosDtos { get; set; } = new List<UserDto>();
}
