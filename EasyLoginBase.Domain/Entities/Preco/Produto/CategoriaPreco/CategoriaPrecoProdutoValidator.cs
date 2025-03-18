using FluentValidation;

namespace EasyLoginBase.Domain.Entities.Preco.Produto.CategoriaPreco;

public class CategoriaPrecoProdutoValidator : AbstractValidator<CategoriaPrecoProdutoEntity>
{
    public CategoriaPrecoProdutoValidator()
    {
        RuleFor(p => p.CategoriaPreco)
            .NotEmpty().WithMessage("A categoria de preço do produto não pode ser vazia.")
            .Length(3, 100).WithMessage("A categoria de preço do produto deve ter entre 3 e 100 caracteres.");
    }
}
