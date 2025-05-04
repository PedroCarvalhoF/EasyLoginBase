using EasyLoginBase.Application.Dto.Produto.Estoque.Movimento;
using EasyLoginBase.Domain.Entities.Produto.Estoque;
using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Services.Tools.UseCase;
public static class ParseMovimentacaoEstoque
{
    public static MovimentoEstoqueDto EstoqueProdutoEntityForDto(MovimentacaoEstoqueProdutoEntity movimentacao, UserEntity[] usuarios)
    {
        var nome_usuario = usuarios.FirstOrDefault(p => p.Id == movimentacao.UsuarioRegistroId).Nome;
        var sobre_nome = usuarios.FirstOrDefault(p => p.Id == movimentacao.UsuarioRegistroId).SobreNome;

        var operacao = movimentacao.Tipo;

        string operacaoDescricao = string.Empty;

        switch (operacao)
        {
            case TipoMovimentacaoEstoque.Entrada:
                operacaoDescricao = "Entrada";
                break;
            case TipoMovimentacaoEstoque.Saida:
                operacaoDescricao = "Saída";
                break;
            case TipoMovimentacaoEstoque.AjustePositivo:
                break;
            case TipoMovimentacaoEstoque.AjusteNegativo:
                break;
            case TipoMovimentacaoEstoque.TransferenciaEntrada:
                break;
            case TipoMovimentacaoEstoque.TransferenciaSaida:
                break;
            default:
                operacaoDescricao = "Não identificada";
                break;
        }

        return new MovimentoEstoqueDto
        {

            ProdutoId = movimentacao.ProdutoId,
            NomeProduto = movimentacao.Produto?.NomeProduto,
            FilialId = movimentacao.FilialId,
            NomeFilial = movimentacao.Filial?.NomeFilial,
            Quantidade = movimentacao.Quantidade,
            Tipo = operacaoDescricao,
            Observacao = movimentacao.Observacao,
            DataMovimentacao = movimentacao.DataMovimentacao,
            UsuarioRegistroId = movimentacao.UsuarioRegistroId,
            NomeUsuarioRegistro = $"{nome_usuario} {sobre_nome}",
        };
    }

    public static IEnumerable<MovimentoEstoqueDto> EstoqueProdutoEntityForDto(this IEnumerable<MovimentacaoEstoqueProdutoEntity> movimentacoes, UserEntity[] usuarios)
    {
        if (movimentacoes == null)
            throw new Exception("Entidade estoque não pode ser nula.");


        return movimentacoes.Select(e => EstoqueProdutoEntityForDto(e, usuarios));
    }

    public static MovimentacaoEstoqueProdutoEntityFiltro DtoForEntity(MovimentoEstoqueDtoFiltro filtro)
    {
        return new MovimentacaoEstoqueProdutoEntityFiltro
        {
            IdMovimento = filtro.IdMovimento,
            ProdutoId = filtro.ProdutoId,
            FilialId = filtro.FilialId,
            Tipo = filtro.Tipo,
            DataMovimentacaoInicial = filtro.DataMovimentacaoInicial,
            DataMovimentacaoFinal = filtro.DataMovimentacaoFinal,
            UsuarioRegistroId = filtro.UsuarioRegistroId
        };
    }
}
