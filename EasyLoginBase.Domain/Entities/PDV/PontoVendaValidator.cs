using FluentValidation;

namespace EasyLoginBase.Domain.Entities.PDV;

public class PontoVendaValidator : AbstractValidator<PontoVendaEntity>
{
    public PontoVendaValidator()
    {
        RuleFor(p => p.FilialPdvId)
            .NotEmpty().WithMessage("O ID da filial é obrigatório.");

        RuleFor(p => p.UsuarioPdvId)
            .NotEmpty().WithMessage("O ID do usuário do PDV é obrigatório.");

        RuleFor(p => p.Cancelado)
            .Must((pontoVenda, cancelado) => !(cancelado && pontoVenda.Aberto))
            .WithMessage("O PDV cancelado não pode estar aberto.");
    }
}
