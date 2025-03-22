using EasyLoginBase.Application.Dto.PDV.PDV;
using EasyLoginBase.Application.Services.Intefaces.PDV;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Interfaces;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.PDV;
public class PontoVendaServices : IPontoVendaServices
{
    private readonly IUnitOfWork _repository;
    public PontoVendaServices(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<PontoVendaDto> AbrirPontoVenda(PontoVendaDtoCreate create, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var pdvEntity = PontoVendaEntity.Create(create.FilialId, create.UsuarioPdvId, clienteId, user_logado);

            await _repository.PontoVendaRepository.NovoPontoVenda(pdvEntity);

            if (await _repository.CommitAsync())
            {
                var pdvCreate = await _repository.PontoVendaRepository
                    .ConsultarPontoVendaByIdPdv(pdvEntity.Id) ??
                    throw new Exception("Erro inesperado.Ponto de venda criado mas não foi possivel consultar.");

                return new PontoVendaDto
                {
                    Id = pdvCreate.Id,
                    Aberto = pdvCreate.Aberto,
                    Cancelado = pdvCreate.Cancelado,
                    CreateAt = pdvCreate.CreateAt,
                    FilialPdv = pdvCreate.FilialPdv!.NomeFilial,
                    FilialPdvId = pdvCreate.FilialPdvId,
                    Usuario = pdvCreate.UsuarioPdv!.UserCaixaPdvEntity!.Nome,
                    UsuarioPdvId = pdvCreate.UsuarioPdvId

                };
            }

            throw new Exception("Não foi possível abrir novo ponto de venda");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<PontoVendaDto>> ConsultarPdvsAbertos(ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            IEnumerable<PontoVendaEntity> pdvsEntities = await _repository.PontoVendaRepository.ConsultarPdvsAbertos(clienteId);

            return pdvsEntities.Select(pdv => new PontoVendaDto
            {
                Id = pdv.Id,
                Aberto = pdv.Aberto,
                Cancelado = pdv.Cancelado,
                CreateAt = pdv.CreateAt,
                FilialPdv = pdv.FilialPdv!.NomeFilial,
                FilialPdvId = pdv.FilialPdvId,
                Usuario = pdv.UsuarioPdv!.UserCaixaPdvEntity!.Nome,
                UsuarioPdvId = pdv.UsuarioPdvId
            });
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<PontoVendaDto>> ConsultarPdvsFiltro(PontoVendaDtoFiltroConsulta filtro, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();
            IEnumerable<PontoVendaEntity> pdvsEntities = Enumerable.Empty<PontoVendaEntity>();

            if (filtro.Id.HasValue)
            {
                var pdvEntity = await _repository.PontoVendaRepository.ConsultarPontoVendaByIdPdv(filtro.Id.Value);
                if (pdvEntity != null)
                {
                    pdvsEntities = new List<PontoVendaEntity> { pdvEntity };
                }
            }

            return pdvsEntities.Select(pdv => new PontoVendaDto
            {
                Id = pdv.Id,
                Aberto = pdv.Aberto,
                Cancelado = pdv.Cancelado,
                CreateAt = pdv.CreateAt,
                FilialPdv = pdv.FilialPdv?.NomeFilial ?? "N/A",
                FilialPdvId = pdv.FilialPdvId,
                Usuario = pdv.UsuarioPdv?.UserCaixaPdvEntity?.Nome ?? "Desconhecido",
                UsuarioPdvId = pdv.UsuarioPdvId

            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao consultar PDVs com filtro: {ex.Message}", ex);
        }
    }

}
