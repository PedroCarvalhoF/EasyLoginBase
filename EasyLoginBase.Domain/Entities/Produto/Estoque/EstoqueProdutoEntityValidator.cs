using FluentValidation;

namespace EasyLoginBase.Domain.Entities.Produto.Estoque;

public class EstoqueProdutoEntityValidator : AbstractValidator<EstoqueProdutoEntity>
{
    public EstoqueProdutoEntityValidator()
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage("ProdutoId é obrigatório.");

        RuleFor(x => x.FilialId)
            .NotEmpty().WithMessage("FilialId é obrigatório.");

        //RuleFor(x => x.Quantidade)
        //    .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.");
    }
}
