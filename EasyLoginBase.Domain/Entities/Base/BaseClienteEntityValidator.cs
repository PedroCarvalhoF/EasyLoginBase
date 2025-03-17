using FluentValidation;

namespace EasyLoginBase.Domain.Entities.Base;

public class BaseClienteEntityValidator : AbstractValidator<BaseClienteEntity>
{
    public BaseClienteEntityValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage("O ClienteId é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("O ClienteId não pode ser um Guid vazio.");

        RuleFor(x => x.UsuarioRegistroId)
            .NotEmpty().WithMessage("O UsuarioRegistroId é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("O UsuarioRegistroId não pode ser um Guid vazio.");

        RuleFor(x => x.CreateAt)
            .LessThanOrEqualTo(x => DateTime.UtcNow).WithMessage("A data de criação não pode estar no futuro.");

        RuleFor(x => x.UpdateAt)
            .GreaterThanOrEqualTo(x => x.CreateAt).WithMessage("A data de atualização não pode ser menor que a data de criação.");
    }
}
