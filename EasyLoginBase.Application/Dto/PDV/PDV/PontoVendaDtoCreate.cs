namespace EasyLoginBase.Application.Dto.PDV.PDV;

public class PontoVendaDtoCreate
{
    public Guid FilialId { get; private set; }
    public Guid UsuarioPdvId { get; private set; }
    public PontoVendaDtoCreate(Guid filialId, Guid usuarioPdvId)
    {
        FilialId = filialId;
        UsuarioPdvId = usuarioPdvId;
    }
}