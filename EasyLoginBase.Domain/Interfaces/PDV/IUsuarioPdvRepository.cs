using EasyLoginBase.Domain.Entities.PDV;

namespace EasyLoginBase.Domain.Interfaces.PDV;

public interface IUsuarioPdvRepository
{
    Task<UsuarioPdvEntity> CadastrarUsuarioPdv(UsuarioPdvEntity entity);
    Task<UsuarioPdvEntity> ConsultarUsuarioPorId(Guid usuarioCaixaPdvEntityId, Guid guid);
    Task<IEnumerable<UsuarioPdvEntity>> ConsultarUsuarios(Guid guid);
}
