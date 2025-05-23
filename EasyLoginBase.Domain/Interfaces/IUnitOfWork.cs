﻿using EasyLoginBase.Domain.Entities;
using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Entities.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces.Cliente;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PDV;
using EasyLoginBase.Domain.Interfaces.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces.Produto.MovimentacaoEstoque;
using EasyLoginBase.Domain.Interfaces.UsuarioClienteVinculo;
using System.Security.Claims;

namespace EasyLoginBase.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
    void FinalizarContexto();
     

    //Cliente Repository
    IGerenericRepository<PessoaClienteEntity> ClienteRepostory { get; }
    IClienteRepository<PessoaClienteEntity> ClienteImplementacao { get; }

    //Usuario Cliente Vinculado
    IGerenericRepository<PessoaClienteVinculadaEntity> UsuarioClienteVinculoRepostory { get; }
    IUsuarioClienteVinculoRepository<PessoaClienteVinculadaEntity> UsuarioClienteVinculoImplementacao { get; }



    IPontoVendaRepository<PontoVendaEntity> PontoVendaRepository { get; }
    IBaseClienteRepository<PontoVendaEntity> PontoVendaRepositoryBase { get; }

    // Método para obter um repositório genérico
    IBaseClienteRepository<T> GetRepository<T>() where T : BaseClienteEntity;
    IGerenericRepository<T> GetGenericRepository<T>() where T : class;


    //REFACTOR NOVO REPOSITORIO E CONTEXTO
    IBaseClienteRepository_REFACTOR<ProdutoEntity> ProdutoRepository { get; }



    //ESTOQUE
    IBaseClienteRepository_REFACTOR<EstoqueProdutoEntity> EstoqueProdutoRepository { get; }
    IEstoqueProdutoRepository<EstoqueProdutoEntity> EstoqueProdutoImplementacao { get; }

    //MOVIMENTACAO ESTOQUE
    IBaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity> MovimentacaoEstoqueProdutoRepository { get; }
    IMovimentacaoEstoqueProdutoRepository<MovimentacaoEstoqueProdutoEntity, FiltroBase> MovimentacaoEstoqueProdutoImplementacao { get; }

    IFilialRepository<FilialEntity, ClaimsPrincipal> FilialRepository { get; }
    IUsuarioPdvRepository UsuarioPdvRepository { get; }


}

