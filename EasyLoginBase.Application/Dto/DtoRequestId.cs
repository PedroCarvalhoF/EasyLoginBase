namespace EasyLoginBase.Application.Dto;
public class DtoRequestId
{
    public Guid Id { get; private set; }

    public DtoRequestId(Guid id)
    {
        Id = id;
    }
}
