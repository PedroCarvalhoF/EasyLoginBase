using FluentValidation;

namespace EasyLoginBase.Domain.Entities.Produto
{
    public class ProdutoValidator : AbstractValidator<ProdutoEntity>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.NomeProduto)
                .NotEmpty().WithMessage("O nome do produto não pode ser vazio.")
                .Length(3, 100).WithMessage("O nome do produto deve ter entre 3 e 100 caracteres.");

            RuleFor(p => p.CodigoProduto)
                .NotEmpty().WithMessage("O código do produto não pode ser vazio.")
                .Length(5, 50).WithMessage("O código do produto deve ter entre 5 e 50 caracteres.");

            RuleFor(p => p.CategoriaProdutoEntityId)
                .NotEmpty().WithMessage("A categoria do produto não pode ser vazia.");
        }
    }
}
