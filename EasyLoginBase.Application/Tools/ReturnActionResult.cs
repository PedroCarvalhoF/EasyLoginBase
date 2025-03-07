using EasyLoginBase.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EasyLoginBase.Application.Tools;
public class ReturnActionResult<T> : Controller
{
    public ActionResult ParseToActionResult(RequestResult<T> requestResult)
    {
        switch (requestResult.StatusCode)
        {
            case (int)HttpStatusCode.OK:
                return Ok(requestResult);
            case (int)HttpStatusCode.BadRequest:
                return BadRequest(requestResult);
            default:
                return BadRequest(requestResult);
        }
    }
}
