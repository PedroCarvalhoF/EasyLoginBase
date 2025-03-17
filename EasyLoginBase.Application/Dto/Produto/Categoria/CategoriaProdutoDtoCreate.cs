using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Application.Dto.Produto.Categoria
{
    public class CategoriaProdutoDtoCreate
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome da categoria deve ter entre 2 e 100 caracteres.")]
        public required string NomeCategoria { get; set; }
    }
}
