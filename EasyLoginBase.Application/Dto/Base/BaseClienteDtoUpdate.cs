using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Application.Dto.Base;

public class BaseClienteDtoUpdate
{
    [Required(ErrorMessage = "O Id é obrigatório.")]
    public Guid Id { get; private set; }

    [Required(ErrorMessage = "O ClienteId é obrigatório.")]
    public Guid ClienteId { get; private set; }
    public BaseClienteDtoUpdate(Guid id, Guid clienteId)
    {
        Id = id;
        ClienteId = clienteId;
    }
}
