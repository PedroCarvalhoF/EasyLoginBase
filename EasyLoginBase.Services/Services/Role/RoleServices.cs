using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Role;
using EasyLoginBase.Application.Services.Intefaces.Role;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Services.Tools.UseCase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.Services.Services.Role;
public class RoleServices : IRoleServices<RoleDto>
{
    private readonly RoleManager<RoleEntity> _roleManager;

    public RoleServices(RoleManager<RoleEntity> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<RequestResult<IEnumerable<RoleDto>>> GetAllRolesAsync()
    {
        try
        {
            var entities = await _roleManager.Roles.ToArrayAsync();

            var dtos = entities.ParseToRole();

            return new RequestResult<IEnumerable<RoleDto>>(dtos);
        }
        catch (Exception ex)
        {

            return new RequestResult<IEnumerable<RoleDto>>(ex);
        }
    }

    public async Task<RequestResult<RoleDto>> GetRoleByIdAsync(Guid id)
    {
        try
        {
            var entity = await _roleManager.Roles.SingleOrDefaultAsync(x => x.Id == id);

            var dto = entity.ParseToRole();

            return new RequestResult<RoleDto>(dto);
        }
        catch (Exception ex)
        {

            return new RequestResult<RoleDto>(ex);
        }
    }
}