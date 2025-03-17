using EasyLoginBase.Application.Dto.Base;

namespace EasyLoginBase.Application.Dto.Produto.Categoria
{
    class CategoriaProdutoDtoUpdate : BaseClienteDtoUpdate
    {
        public required string NomeCategoria { get; set; }
        public CategoriaProdutoDtoUpdate(string nomeCategoria, Guid id, Guid clienteId) : base(id, clienteId)
        {
            NomeCategoria = nomeCategoria;
        }
    }
}
