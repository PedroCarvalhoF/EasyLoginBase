using FluentValidation;

namespace EasyLoginBase.Domain.Entities.PDV;

public class UsuarioPdvValidation : AbstractValidator<UsuarioPdvEntity>
{
    public UsuarioPdvValidation()
    {
        RuleFor(x => x.UsuarioCaixaPdvEntityId)
            .NotEmpty().WithMessage("O ID do usuário do PDV é obrigatório.");

        RuleFor(x => x.AcessoCaixa)
            .Equal(true).WithMessage("O acesso ao caixa é obrigatório.");
    }
}
