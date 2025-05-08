using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Role;
using EasyLoginBase.Application.Services.Intefaces.Role;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class RoleController : ControllerBase 
{
    //private readonly IRoleServices<RoleDto> _roleServices;
    //public RoleController(IRoleServices<RoleDto> roleServices)
    //{
    //    _roleServices = roleServices;
    //}

    //[HttpGet]
    //public async Task<ActionResult<RequestResult<IEnumerable<RoleDto>>>> GetAllRoles()
    //{
    //    try
    //    {
    //        return new ReturnActionResult<IEnumerable<RoleDto>>()
    //            .ParseToActionResult(await _roleServices.GetAllRolesAsync());
    //    }
    //    catch (Exception ex)
    //    {
    //        return new ReturnActionResult<IEnumerable<RoleDto>>().ParseToActionResult(RequestResult<IEnumerable<RoleDto>>.BadRequest(ex.Message));
    //    }
    //}
}
