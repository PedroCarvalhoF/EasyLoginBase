using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;
using MediatR;

namespace EasyLoginBase.Services.CQRS.Filial.Command;

public class FilialCommandCadastrarFilial : BaseCommands<FilialDto>
{
    public required FiliaDtoCreateRequest FiliaDtoCreateRequest { get; set; }

    public class FilialCommandCadastrarFilialHandler : IRequestHandler<FilialCommandCadastrarFilial, RequestResult<FilialDto>>
    {
        private readonly IUnitOfWork _repository;

        public FilialCommandCadastrarFilialHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<RequestResult<FilialDto>> Handle(FilialCommandCadastrarFilial request, CancellationToken cancellationToken)
        {
            try
            {
                var filialEntity = FilialEntity.Create(request.FiliaDtoCreateRequest.NomeFilial);

                var result = await _repository.FilialRepository.CreateFilialAsync(filialEntity);

                if (!await _repository.CommitAsync())
                    return RequestResult<FilialDto>.BadRequest("Não foi possível cadastrar nova filial.");

                return RequestResult<FilialDto>.Ok(DtoMapper.ParceFilial(result));
            }
            catch (Exception ex)
            {

                return RequestResult<FilialDto>.BadRequest(ex.Message);
            }
        }
    }
}
