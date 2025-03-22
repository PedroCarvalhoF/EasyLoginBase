namespace EasyLoginBase.Application.Dto.PDV.PDV;

public class PontoVendaDto
{
    public Guid Id { get; set; }
    public bool Aberto { get; set; }
    public bool Cancelado { get; set; }
    public Guid FilialPdvId { get; set; }
    public string? FilialPdv { get; set; }
    public Guid UsuarioPdvId { get; set; }
    public string? Usuario { get; set; }
    public DateTime CreateAt { get; set; }
    public string Descricao => ToString();
    public override string ToString()
    {
        return $"PDV " +
               $"Cancelado: {(Cancelado ? "Sim" : "Não")} " +
               $"Filial: {FilialPdv ?? "N/A"} " +
               $"Usuário: {Usuario ?? "N/A"} " +
               $"Aberto em: {CreateAt:dd/MM/yyyy HH:mm:ss}";
    }
}
