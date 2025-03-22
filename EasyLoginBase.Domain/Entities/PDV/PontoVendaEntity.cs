using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;
using FluentValidation;
using FluentValidation.Results;

namespace EasyLoginBase.Domain.Entities.PDV;

public class PontoVendaEntity : BaseClienteEntity
{
    public bool Aberto { get; private set; }
    public bool Cancelado { get; private set; }
    public Guid FilialPdvId { get; private set; }
    public virtual FilialEntity? FilialPdv { get; private set; }
    public Guid UsuarioPdvId { get; private set; }
    public virtual UsuarioPdvEntity? UsuarioPdv { get; private set; }
    public PontoVendaEntity() { }
    private PontoVendaEntity(bool aberto, bool cancelado, Guid filialPdvId, Guid usuarioPdvId, Guid clienteId, Guid usuarioRegistroId)
        : base(clienteId, usuarioRegistroId)
    {
        Aberto = aberto;
        Cancelado = cancelado;
        FilialPdvId = filialPdvId;
        UsuarioPdvId = usuarioPdvId;

        Validar();
    }

    public static PontoVendaEntity Create(Guid filialPdvId, Guid usuarioPdvId, Guid clienteId, Guid usuarioRegistroId)
        => new PontoVendaEntity(true, false, filialPdvId, usuarioPdvId, clienteId, usuarioRegistroId);

    public void Abrir()
    {
        if (Aberto)
            throw new InvalidOperationException("O PDV já está aberto.");

        if (Cancelado)
            throw new InvalidOperationException("O PDV foi cancelado e não pode ser reaberto.");

        Aberto = true;
        AtualizarData();
    }

    public void Fechar()
    {
        if (!Aberto)
            throw new InvalidOperationException("O PDV já está fechado.");

        Aberto = false;
        AtualizarData();
    }

    public void Cancelar()
    {
        if (Cancelado)
            throw new InvalidOperationException("O PDV já foi cancelado.");

        Cancelado = true;
        Aberto = false;
        AtualizarData();
    }

    private void Validar()
    {
        var validator = new PontoVendaValidator();
        ValidationResult result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {errors}");
        }
    }
}
