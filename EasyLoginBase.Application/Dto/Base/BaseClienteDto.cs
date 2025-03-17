namespace EasyLoginBase.Application.Dto.Base;

public abstract class BaseClienteDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid UsuarioRegistroId { get; set; }
    public bool Habilitado { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}
