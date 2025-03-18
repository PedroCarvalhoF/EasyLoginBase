using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Domain.Entities.Base;

public abstract class BaseClienteEntity
{
    public Guid Id { get; protected set; }
    public Guid ClienteId { get; protected set; }
    public Guid UsuarioRegistroId { get; protected set; }
    public bool Habilitado { get; private set; }
    public DateTime CreateAt { get; private set; }
    public DateTime UpdateAt { get; private set; }
    protected BaseClienteEntity() { } // Necessário para o EF Core
    protected BaseClienteEntity(Guid clienteId, Guid usuarioRegistroId)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        UsuarioRegistroId = usuarioRegistroId;
        Habilitado = true;
        CreateAt = DateTime.Now;
        UpdateAt = CreateAt;
    }
    public void Ativar()
    {
        if (Habilitado)
            throw new InvalidOperationException("A entidade já está habilitada.");

        Habilitado = true;
        AtualizarData();
    }
    public void Desativar()
    {
        if (!Habilitado)
            throw new InvalidOperationException("A entidade já está desabilitada.");

        Habilitado = false;
        AtualizarData();
    }

    protected void AtualizarData()
    => UpdateAt = DateTime.UtcNow;

    public void ValidarBaseClienteEntity()
    {
        var validator = new BaseClienteEntityValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {errors}");
        }
    }
}
