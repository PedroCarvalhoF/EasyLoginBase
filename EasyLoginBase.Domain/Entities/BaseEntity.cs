namespace EasyLoginBase.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreateAt { get; protected set; }
    public DateTime? UpdateAt { get; protected set; }
    public bool Habilitado { get; protected set; }
    public void DataCriacao(DateTime dataParaAtualizar) => CreateAt = dataParaAtualizar;
    public void Atualizacao() => UpdateAt = DateTime.Now;
    public void Habilitar()
    {
        Habilitado = true;
        UpdateAt = DateTime.Now;
    }
    public void Desabilitar()
    {
        Habilitado = false;
        UpdateAt = DateTime.Now;
    }
}
