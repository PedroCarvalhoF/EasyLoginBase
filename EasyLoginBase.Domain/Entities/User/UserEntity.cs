﻿using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Domain.Entities.User;

public class UserEntity : IdentityUser<Guid>
{
    public string? Nome { get; private set; }
    public string? SobreNome { get; private set; }
    public virtual ICollection<UserRoleEntity>? UserRoles { get; private set; }
    public virtual ICollection<PessoaClienteEntity>? PessoasClientes { get; private set; }
    public virtual ICollection<PessoaClienteVinculadaEntity>? ClientesVinculados { get; set; }
    public virtual UsuarioPdvEntity? UsuarioPdv { get; set; }

    public UserEntity() { }
    public UserEntity(string nome, string sobreNome, string userName, string email)
    {
        Nome = nome;
        SobreNome = sobreNome;
        UserName = userName;
        Email = email;
        EmailConfirmed = true;
    }
    public static UserEntity Create(string nome, string sobreNome, string userName, string email)
        => new(nome, sobreNome, userName, email);
}
