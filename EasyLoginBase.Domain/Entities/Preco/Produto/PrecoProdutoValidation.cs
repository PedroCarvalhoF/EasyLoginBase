using FluentValidation;

namespace EasyLoginBase.Domain.Entities.Preco.Produto;

public class PrecoProdutoValidation : AbstractValidator<PrecoProdutoEntity>
{
    public PrecoProdutoValidation()
    {
        RuleFor(p => p.ProdutoEntityId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.");

        RuleFor(p => p.FilialEntityId)
            .NotEmpty().WithMessage("O ID da filial é obrigatório.");

        RuleFor(p => p.CategoriaPrecoProdutoEntityId)
            .NotEmpty().WithMessage("O ID da categoria de preço é obrigatório.");

        RuleFor(p => p.PrecoProduto)
            .GreaterThanOrEqualTo(0).WithMessage("O preço do produto não pode ser menor do que zero.");

        RuleFor(p => p.TipoPrecoProdutoEnum)
            .IsInEnum().WithMessage("O tipo de preço informado é inválido.");
    }
}
