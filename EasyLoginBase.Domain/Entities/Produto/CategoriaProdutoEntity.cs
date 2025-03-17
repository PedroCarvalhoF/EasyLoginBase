using EasyLoginBase.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Domain.Entities.Produto;
public class CategoriaProdutoEntity : BaseClienteEntity
{
    public string? NomeCategoria { get; private set; }
    public CategoriaProdutoEntity() { }
    private CategoriaProdutoEntity(string nomeCategoria, Guid clienteId, Guid usuarioRegistroId) : base(clienteId, usuarioRegistroId)
    {
        DefinirNome(nomeCategoria);
    }

    // Método para criar a entidade garantindo consistência
    public static CategoriaProdutoEntity Criar(string nomeCategoria, Guid clienteId, Guid usuarioRegistroId)
    {
        if (string.IsNullOrWhiteSpace(nomeCategoria))
            throw new ArgumentException("O nome da categoria é obrigatório.", nameof(nomeCategoria));

        return new CategoriaProdutoEntity(nomeCategoria, clienteId, usuarioRegistroId);
    }

    // Método para alterar o nome da categoria de forma segura
    public void AlterarNome(string novoNome)
    {
        DefinirNome(novoNome); AtualizarData();
    }

    // Encapsula a lógica de definição do nome
    private void DefinirNome(string nomeCategoria)
    {
        if (string.IsNullOrWhiteSpace(nomeCategoria))
            throw new ArgumentException("O nome da categoria não pode ser vazio ou nulo.", nameof(nomeCategoria));

        NomeCategoria = nomeCategoria;
    }

    public void ValidarCategoria()
    {
        ValidarBaseClienteEntity();

        var validator = new CategoriaProdutoValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {errors}");
        }
    }
}