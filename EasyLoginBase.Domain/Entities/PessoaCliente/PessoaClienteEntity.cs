using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.User;
using System.Runtime.InteropServices;

namespace EasyLoginBase.Domain.Entities.PessoaCliente;

public class PessoaClienteEntity
{
    public Guid Id { get; set; }
    public Guid UsuarioEntityClienteId { get; set; }
    public virtual UserEntity? UsuarioEntityCliente { get; set; }
    public string? NomeFantasia { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataVencimentoUso { get; set; }
    public bool AcessoLiberado => ValidarAcessoClienteUsuarios();
    public string StatusCliente => GetStatusCliente();
    private string GetStatusCliente()
    {
        var qtd_dias_restantes = (DataVencimentoUso - DateTime.Now).TotalDays;

        if (!AcessoLiberado)
        {
            if (qtd_dias_restantes < 0)
                return $"Acesso bloqueado. Verifquei as pendencias com financeiro.";
        }

        return $"Acesso liberado: dias restantes {qtd_dias_restantes}";
    }
    private bool ValidarAcessoClienteUsuarios()
    {
        if (DataVencimentoUso < DateTime.Now)
            return false;

        return true;
    }

    public virtual ICollection<PessoaClienteVinculadaEntity>? UsuariosVinculados { get; set; }
    public virtual ICollection<FilialEntity>? Filiais { get; set; }
    public bool EntidadeValidada => ValidarEntidade();
    private bool ValidarEntidade()
    => true;
    public PessoaClienteEntity() { }
    public PessoaClienteEntity(Guid usuarioEntityClienteId, string nomeFantasia)
    {
        Id = Guid.NewGuid();
        UsuarioEntityClienteId = usuarioEntityClienteId;
        NomeFantasia = nomeFantasia;
        DataAbertura = DateTime.Now;
        DataVencimentoUso = DateTime.Now.AddMonths(1);
    }
    public static PessoaClienteEntity CriarUsuarioPessoaCliente(Guid usuarioEntityClienteId, string nomeFantasia)
    => new PessoaClienteEntity(usuarioEntityClienteId, nomeFantasia);
}
