using FluentValidation;

namespace EasyLoginBase.Domain.Entities.Produto;

public class CategoriaProdutoValidator : AbstractValidator<CategoriaProdutoEntity>
{
    public CategoriaProdutoValidator()
    {
        RuleFor(x => x.NomeCategoria)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da categoria deve ter no máximo 100 caracteres.");
    }
}
