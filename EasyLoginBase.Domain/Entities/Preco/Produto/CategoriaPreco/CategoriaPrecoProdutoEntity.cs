using EasyLoginBase.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Domain.Entities.Preco.Produto.CategoriaPreco;

public class CategoriaPrecoProdutoEntity : BaseClienteEntity
{
    public string? CategoriaPreco { get; private set; }
    public bool EntidadeValidada => ValidarCategoriaPrecoProduto();
    public CategoriaPrecoProdutoEntity() { }
    CategoriaPrecoProdutoEntity(string categoriaPreco, Guid clienteId, Guid usuarioRegistroId) : base(clienteId, usuarioRegistroId)
    {
        CategoriaPreco = categoriaPreco;
    }
    public static CategoriaPrecoProdutoEntity CriarCategoriaPrecoProduto(string categoriaPreco, Guid clienteId, Guid usuarioRegistroId)
    => new CategoriaPrecoProdutoEntity(categoriaPreco, clienteId, usuarioRegistroId);
    private bool ValidarCategoriaPrecoProduto()
    {
        var validator = new CategoriaPrecoProdutoValidator();
        var resultado = validator.Validate(this);

        if (!resultado.IsValid)
        {
            var erros = string.Join("; ", resultado.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {erros}");
        }

        var validarBaseCliente = new BaseClienteEntityValidator();
        var resultadoBaseCliente = validarBaseCliente.Validate(this);

        if (!resultadoBaseCliente.IsValid)
        {
            var erros = string.Join("; ", resultado.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {erros}");
        }

        return true;
    }
}
