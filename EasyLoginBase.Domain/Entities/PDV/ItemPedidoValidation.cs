using FluentValidation;

namespace EasyLoginBase.Domain.Entities.PDV;

public class ItemPedidoValidation : AbstractValidator<ItemPedidoEntity>
{
    public ItemPedidoValidation()
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.");

        RuleFor(x => x.PedidoId)
            .NotEmpty().WithMessage("O ID do pedido é obrigatório.");

        RuleFor(x => x.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

        RuleFor(x => x.Preco)
            .GreaterThanOrEqualTo(0).WithMessage("O preço não pode ser negativo.");

        RuleFor(x => x.Desconto)
            .GreaterThanOrEqualTo(0).WithMessage("O desconto não pode ser negativo.")
            .Must((item, desconto) => desconto <= item.SubTotal)
            .WithMessage("O desconto não pode ser maior que o subtotal.");

        RuleFor(x => x.Observacao)
            .MaximumLength(500).WithMessage("A observação não pode ultrapassar 500 caracteres.");
    }
}
