namespace EasyLoginBase.Application.Dto.PDV.PDV
{
    public class PontoVendaDtoRequestFiltro
    {
        public Guid? Id { get; set; }
        public bool? Aberto { get; set; }
        public bool? Cancelado { get; set; }
        public Guid? FilialPdvId { get; set; }
        public Guid? UsuarioPdvId { get; set; }

        // Paginação e ordenação (opcional, mas útil para consultas futuras)
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string? OrderBy { get; set; } // Ex: "DataAbertura desc"

        // Método para validar filtros (caso necessário)
        public bool TemFiltro()
        {
            return Id.HasValue || Aberto.HasValue || Cancelado.HasValue ||
                   FilialPdvId.HasValue || UsuarioPdvId.HasValue;
        }
    }
}
