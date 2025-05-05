using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User.Role;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Services.Services.User.Roles;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class AccountUserRoleControlle : ControllerBase
{
    private readonly IUserRoleServices<RoleDto, RoleUserDto> _userRoleServices;

    public AccountUserRoleControlle(IUserRoleServices<RoleDto, RoleUserDto> userRoleServices)
    {
        _userRoleServices = userRoleServices;
    }

    [HttpGet]
    public async Task<ActionResult<RequestResult<IEnumerable<RoleDto>>>> SelectRolesAscyn()
    {
        try
        {
            return new ReturnActionResult<IEnumerable<RoleDto>>().ParseToActionResult(await _userRoleServices.SelectAsync());
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<RoleDto>>().ParseToActionResult(RequestResult<IEnumerable<RoleDto>>.BadRequest(ex.Message));
        }
    }

    [HttpPost("select-roles-user")]
    public async Task<ActionResult<RequestResult<RoleUserDto>>> SelectRolesUserAscyn([FromBody] DtoRequestId requestId)
    {
        try
        {
            return new ReturnActionResult<RoleUserDto>().ParseToActionResult(await _userRoleServices.SelectRolesUserAscyn(requestId));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<RoleUserDto>().ParseToActionResult(RequestResult<RoleUserDto>.BadRequest(ex.Message));
        }
    }

    [HttpPost("select-users-role")]
    public async Task<ActionResult<RequestResult<IEnumerable<RoleUserDto>>>> SelectUsersByRole([FromBody] DtoRequestId requestRoleId)
    {
        try
        {
            return new ReturnActionResult<IEnumerable<RoleUserDto>>().ParseToActionResult(await _userRoleServices.SelectUsersByRole(requestRoleId));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<RoleUserDto>>().ParseToActionResult(RequestResult<IEnumerable<RoleUserDto>>.BadRequest(ex.Message));
        }
    }


    [HttpPost]
    public async Task<ActionResult<RequestResult<RoleEntity>>> CreateRoleAsync([FromBody] RoleDtoCreate roleDtoCreate)
    {
        try
        {
            return new ReturnActionResult<RoleDto>().ParseToActionResult(await _userRoleServices.CreateRoleAsync(roleDtoCreate));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<RoleDto>().ParseToActionResult(RequestResult<RoleDto>.BadRequest(ex.Message));
        }
    }

    [HttpPost("AddRoleUser")]
    public async Task<ActionResult<RequestResult<RoleUserDto>>> AdcionarRoleUser([FromBody] RoleDtoAddRoleUser roleDtoAddRoleUser)
    {
        try
        {
            return new ReturnActionResult<RoleUserDto>().ParseToActionResult(await _userRoleServices.AdcionarRoleUser(roleDtoAddRoleUser));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<RoleUserDto>().ParseToActionResult(RequestResult<RoleUserDto>.BadRequest(ex.Message));
        }
    }

    [HttpPost("RemoverRoleUser")]
    public async Task<ActionResult<RequestResult<RoleUserDto>>> RemoverRoleUser([FromBody] RoleDtoRemoverRoleUser roleDtoRemoverRoleUser)
    {
        try
        {
            return new ReturnActionResult<RoleUserDto>().ParseToActionResult(await _userRoleServices.RemovarRoleUser(roleDtoRemoverRoleUser));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<RoleUserDto>().ParseToActionResult(RequestResult<RoleUserDto>.BadRequest(ex.Message));
        }
    }
}