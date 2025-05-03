using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.User;
using FluentValidation;
using FluentValidation.Results;

namespace EasyLoginBase.Domain.Entities.PDV;

public class UsuarioPdvEntity : BaseClienteEntity
{
    public bool AcessoCaixa { get; private set; }
    public Guid UsuarioCaixaPdvEntityId { get; private set; }
    public virtual UserEntity? UserCaixaPdvEntity { get; private set; }
    public virtual ICollection<PontoVendaEntity>? PontoVendaEntities { get; private set; }
    private UsuarioPdvEntity(bool acessoCaixa, Guid usuarioCaixaPdvEntityId, Guid clienteId, Guid usuarioRegistroId)
        : base(clienteId, usuarioRegistroId)
    {
        AcessoCaixa = acessoCaixa;
        UsuarioCaixaPdvEntityId = usuarioCaixaPdvEntityId;

        Validar();
    }

    public static UsuarioPdvEntity Create(Guid usuarioCaixaPdvEntityId, Guid clienteId, Guid usuarioRegistroId, bool acessoCaixa = true)
        => new(acessoCaixa, usuarioCaixaPdvEntityId, clienteId, usuarioRegistroId);

    private void AlterarAcessoCaixa(bool status, string mensagemErro)
    {
        if (AcessoCaixa == status)
            throw new InvalidOperationException(mensagemErro);

        AcessoCaixa = status;
        AtualizarData();
    }

    public void AtivarAcessoCaixa() => AlterarAcessoCaixa(true, "O acesso ao caixa já está habilitado.");
    public void DesativarAcessoCaixa() => AlterarAcessoCaixa(false, "O acesso ao caixa já está desabilitado.");
    private void Validar()
    {
        var validator = new UsuarioPdvValidation();
        ValidationResult result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {errors}");
        }

        AtualizarData();
    }
}
