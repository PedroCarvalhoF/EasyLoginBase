using EasyLoginBase.Application.Dto;
using MediatR;

namespace EasyLoginBase.Services.CQRS;

public class BaseCommands<T> : IRequest<RequestResult<T>> where T : class
{

}
