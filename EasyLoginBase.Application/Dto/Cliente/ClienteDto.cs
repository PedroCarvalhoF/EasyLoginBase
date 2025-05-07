using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;

namespace EasyLoginBase.Application.Dto.Cliente;
public class ClienteDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string? ClienteNome { get; set; }
    public string? ClienteEmail { get; set; }
    public string? NomeFantasia { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataVencimentoUso { get; set; }
    public virtual List<UsuarioVinculadoClienteDto> UsuariosVinculados { get; set; } = new List<UsuarioVinculadoClienteDto>();
    public virtual List<FilialDto> Filiais { get; set; } = new List<FilialDto>();
}