using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EasyLoginBase.Application.Dto;

public class RequestResult<T>
{
    public bool Status { get; }
    public int StatusCode { get; }
    public string? Mensagem { get; }
    public T? Data { get; }

    public RequestResult(T dados)
    {
        Status = true;
        StatusCode = 200;
        Data = dados;
        Mensagem = "Requisição realizada com sucesso.";
    }

    public RequestResult(Exception ex)
    {
        Status = false;
        StatusCode = 400;
        Data = default;
        Mensagem = ex.Message;
    }


    private RequestResult(bool status, int statusCode, string? mensagem, T? data = default)
    {
        Status = status;
        StatusCode = statusCode;
        Mensagem = mensagem;
        Data = data;
    }

    public static RequestResult<T> Ok(T? data = default, string mensagem = "Requisição realizada com sucesso.")
        => new(true, 200, mensagem, data);

    public static RequestResult<T> BadRequest(string mensagem = "Não foi possível realizar a requisição.")
        => new(false, 400, mensagem);

    public static RequestResult<T> BadRequest(T data, string mensagem = "Não foi possível realizar a requisição.")
        => new(false, 400, mensagem, data);

    public static RequestResult<T> Unauthorized(string mensagem = "Acesso não autorizado.")
        => new(false, 401, mensagem);

    public static RequestResult<T> NotFound(string mensagem = "Recurso não encontrado.")
        => new(false, 404, mensagem);

    public static RequestResult<T> ServerError(string mensagem = "Erro interno no servidor.")
        => new(false, 500, mensagem);
}

