using EasyLoginBase.Domain.Entities.Filial;
using FluentValidation;

public class FilialValidator : AbstractValidator<FilialEntity>
{
    public FilialValidator()
    {
        RuleFor(f => f.NomeFilial)
            .NotEmpty().WithMessage("O nome da filial é obrigatório.")
            .MinimumLength(3).WithMessage("O nome da filial deve ter pelo menos 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome da filial não pode ter mais de 100 caracteres.");

        RuleFor(f => f.PessoaClienteId)
            .NotEmpty().WithMessage("O identificador do cliente é obrigatório.");
    }
}
