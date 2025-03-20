using EasyLoginBase.Application.Dto.PDV.Usuario;
using EasyLoginBase.Application.Services.Intefaces.PDV;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Interfaces;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.PDV;

public class UsuarioPdvServices : IUsuarioPdvServices
{
    private readonly IUnitOfWork _repository;

    public UsuarioPdvServices(IUnitOfWork repository)
    {
        _repository = repository;
    }
    public async Task<UsuarioPdvDto> CadastrarUsuarioPdv(UsuarioPdvDtoCreate newUserPdv, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            UsuarioPdvEntity entity = UsuarioPdvEntity.Create(newUserPdv.UsuarioCaixaPdvEntityId, clienteId, user_logado);

            UsuarioPdvEntity entityCreateResult = await _repository.UsuarioPdvRepository.CadastrarUsuarioPdv(entity);

            if (await _repository.CommitAsync())
            {

                var entityCreate = await _repository.UsuarioPdvRepository.ConsultarUsuarioPorId(entity.UsuarioCaixaPdvEntityId, clienteId);

                return new UsuarioPdvDto
                {
                    AcessoCaixa = entityCreate.AcessoCaixa,
                    UsuarioCaixaPdvEntityId = entityCreate.UsuarioCaixaPdvEntityId,
                    UsuarioCaixaPdvEntityNome = entityCreate.UserCaixaPdvEntity?.Nome,
                    Email = entityCreate.UserCaixaPdvEntity?.Email
                };
            }

            throw new Exception("Erro ao cadastrar usuário pdv.");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<UsuarioPdvDto> ConsultarUsuarioPorId(UsuarioPdvDtoRequestId usuarioPdvDtoRequestId, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            UsuarioPdvEntity entity = await _repository.UsuarioPdvRepository.ConsultarUsuarioPorId(usuarioPdvDtoRequestId.UsuarioCaixaPdvEntityId, user.GetClienteIdVinculo());

            if (entity == null)
                throw new Exception("Usuário não encontrado.");

            return new UsuarioPdvDto
            {
                AcessoCaixa = entity.AcessoCaixa,
                UsuarioCaixaPdvEntityId = entity.UsuarioCaixaPdvEntityId,
                UsuarioCaixaPdvEntityNome = entity.UserCaixaPdvEntity?.Nome,
                Email = entity.UserCaixaPdvEntity?.Email

            };
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<UsuarioPdvDto>> ConsultarUsuarios(ClaimsPrincipal user)
    {
        try
        {
            IEnumerable<UsuarioPdvEntity>
                 entities = await _repository.UsuarioPdvRepository.ConsultarUsuarios(user.GetClienteIdVinculo());
            if (entities == null)
                throw new Exception("Usuários não encontrados.");

            return entities.Select(entity => new UsuarioPdvDto
            {
                AcessoCaixa = entity.AcessoCaixa,
                UsuarioCaixaPdvEntityId = entity.UsuarioCaixaPdvEntityId,
                UsuarioCaixaPdvEntityNome = entity.UserCaixaPdvEntity?.Nome,
                Email = entity.UserCaixaPdvEntity?.Email
            });
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<UsuarioPdvDto> AtivarAcessoCaixa(UsuarioPdvDtoRequestId usuarioPdvDtoRequestId, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
    public Task<UsuarioPdvDto> DesativarAcessoCaixa(UsuarioPdvDtoRequestId usuarioPdvDtoRequestId, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }

    
}
