namespace EasyLoginBase.Application.Dto.PDV.Usuario;

public class UsuarioPdvDtoCreate
{
    public Guid UsuarioCaixaPdvEntityId { get; private set; }
    public UsuarioPdvDtoCreate(Guid usuarioCaixaPdvEntityId)
    {
        UsuarioCaixaPdvEntityId = usuarioCaixaPdvEntityId;
    }
}
