using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Services.Intefaces;
using MediatR;

namespace EasyLoginBase.Services.CQRS.Filial.Command;

public class FilialCommandGetFiliais : BaseCommands<IEnumerable<FilialDto>>
{
    public class FilialCommandGetFiliaisHandler : IRequestHandler<FilialCommandGetFiliais, RequestResult<IEnumerable<FilialDto>>>
    {
        private readonly IFilialServices<FilialDto> _filialServices;
        public FilialCommandGetFiliaisHandler(IFilialServices<FilialDto> filialServices)
        {
            _filialServices = filialServices;
        }
        public async Task<RequestResult<IEnumerable<FilialDto>>> Handle(FilialCommandGetFiliais request, CancellationToken cancellationToken)
        {
            try
            {
                var dtos = await _filialServices.SelectFilialAsync();
                return RequestResult<IEnumerable<FilialDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {

                return RequestResult<IEnumerable<FilialDto>>.BadRequest(ex.Message);
            }
        }
    }
}
