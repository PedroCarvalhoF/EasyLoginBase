using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Application.Dto.User.Role;
using EasyLoginBase.Application.Tools.Roles;
using EasyLoginBase.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.Services.Services.User.Roles;
public class UserRoleServices : IUserRoleServices<RoleDto, RoleUserDto>
{
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly UserManager<UserEntity> _userManager;
    public UserRoleServices(RoleManager<RoleEntity> roleManager, UserManager<UserEntity> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public async Task<RequestResult<IEnumerable<RoleDto>>> SelectAsync()
    {
        var entities = await _roleManager.Roles.OrderBy(r => r.Name).ToArrayAsync();

        var dto = entities.Select(e => new RoleDto
        {
            Id = e.Id,
            RoleName = e.Name,
        });

        return new RequestResult<IEnumerable<RoleDto>>(dto);
    }
    public async Task<RequestResult<RoleDto>> CreateRoleAsync(RoleDtoCreate roleDtoCreate)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleDtoCreate.NomeRole);
        if (!roleExists)
        {
            var role = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = roleDtoCreate.NomeRole,
                NormalizedName = roleDtoCreate.NomeRole.ToUpper()
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return RequestResult<RoleDto>.BadRequest(errors.SingleOrDefault().ToString());

            }


            var dto = new RoleDto
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            return new RequestResult<RoleDto>(dto);
        }

        return RequestResult<RoleDto>.BadRequest("Não foi possível criar role");
    }
    public async Task<RequestResult<RoleUserDto>> AdcionarRoleUser(RoleDtoAddRoleUser roleDtoAddRoleUser)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(roleDtoAddRoleUser.RoleId.ToString());
            if (role == null) return RequestResult<RoleUserDto>.BadRequest($"Role não encontrada");

            var user = await _userManager.FindByIdAsync(roleDtoAddRoleUser.UserId.ToString());
            if (user == null) return RequestResult<RoleUserDto>.BadRequest($"Usuário não encontrado");

            var result = await _userManager.AddToRoleAsync(user, role.Name!);

            var userRoles = await _userManager.GetRolesAsync(user);

            var roleUserDto = new RoleUserDto
            {
                UserDto = new UserDto
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    SobreNome = user.SobreNome,
                    Email = user.Email
                },
                Roles = userRoles.ToList()
            };

            return new RequestResult<RoleUserDto>(roleUserDto);
        }
        catch (Exception ex)
        {

            return new RequestResult<RoleUserDto>(ex);
        }
    }
    public async Task<RequestResult<RoleUserDto>> RemovarRoleUser(RoleDtoRemoverRoleUser roleDtoRemoverRoleUser)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(roleDtoRemoverRoleUser.RoleId.ToString());
            if (role == null)
                return RequestResult<RoleUserDto>.BadRequest("Role não encontrada");

            var user = await _userManager.FindByIdAsync(roleDtoRemoverRoleUser.UserId.ToString());
            if (user == null)
                return RequestResult<RoleUserDto>.BadRequest("Usuário não encontrado");

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name!);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return RequestResult<RoleUserDto>.BadRequest($"Erro ao remover role: {errors}");
            }

            var rolesAtualizadas = await _userManager.GetRolesAsync(user);

            var dto = new RoleUserDto
            {
                UserDto = new UserDto
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    SobreNome = user.SobreNome,
                    Email = user.Email
                },
                Roles = rolesAtualizadas.ToList()
            };

            return new RequestResult<RoleUserDto>(dto);
        }
        catch (Exception ex)
        {
            return new RequestResult<RoleUserDto>(ex);
        }
    }
    public async Task<RequestResult<RoleUserDto>> SelectRolesUserAscyn(DtoRequestId requestId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(requestId.Id.ToString() ?? string.Empty);
            if (user == null) return RequestResult<RoleUserDto>.BadRequest($"Usuário não encontrado");

            var userRoles = await _userManager.GetRolesAsync(user);

            var roleUserDto = new RoleUserDto
            {
                UserDto = new UserDto
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    SobreNome = user.SobreNome,
                    Email = user.Email
                },
                Roles = userRoles.ToList()
            };

            return new RequestResult<RoleUserDto>(roleUserDto);
        }
        catch (Exception ex)
        {

            return new RequestResult<RoleUserDto>(ex);
        }
    }
    public async Task<RequestResult<IEnumerable<RoleUserDto>>> SelectUsersByRole(DtoRequestId requestRoleId)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(requestRoleId.Id.ToString());
            if (role == null)
                return RequestResult<IEnumerable<RoleUserDto>>.BadRequest("Role não encontrada");

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);

            var result = await Task.WhenAll(usersInRole.Select(async user => new RoleUserDto
            {
                UserDto = new UserDto
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    SobreNome = user.SobreNome,
                    Email = user.Email
                },
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            }));

            return new RequestResult<IEnumerable<RoleUserDto>>(result);
        }
        catch (Exception ex)
        {
            return new RequestResult<IEnumerable<RoleUserDto>>(ex);
        }
    }
    public async Task CriarRoles()
    {
        foreach (var roleName in Enum.GetNames(typeof(RolesEnum)))
        {
            var exists = await _roleManager.RoleExistsAsync(roleName);
            if (!exists)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };

                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Erro ao criar role '{roleName}': {errors}");
                }
            }
        }
    }
}
