namespace EasyLoginBase.Application.Dto;

public class RequestResult<T> where T : class
{
    public bool Status { get; set; } = false;
    public int StatusCode { get; private set; } = 400;
    public string? Mensagem { get; private set; }
    public T? Data { get; private set; }
    public RequestResult()
    {

    }
    RequestResult(T? data, string? mensagem)
    {
        Status = true;
        StatusCode = 200;

        if (string.IsNullOrEmpty(mensagem))
            Mensagem = "Requisição realizada com sucesso.";
        else
            Mensagem = mensagem;

        Data = data;
    }

    RequestResult(string? mensagem)
    {
        Status = false;
        StatusCode = 400;

        if (string.IsNullOrEmpty(mensagem))
            Mensagem = "Não foi possível relizar requisição.";
        else
            Mensagem = mensagem;
        Data = null;
    }

    public RequestResult<T> ResultOk(T data)
    => Ok(data);
    public RequestResult<T> Erro(Exception ex)
    => BadRequest(ex.Message);
    public RequestResult<T> Erro(string mensagem)
    => BadRequest(mensagem);

    public RequestResult<T> EntidadeInvalida()
        => EntidadeInvalida();
    public RequestResult<T> SemParamentroConsulta()
    => BadRequest("Nenhum parametro foi encontrado para realizar consulta");

    public RequestResult<T> ErroSalvarNoBanco()
     => BadRequest("Não foi possível salvar no banco de dados.");

    public static RequestResult<T> Ok(T? data = null, string? mensagem = "Requesição realizada com sucesso.")
    => new RequestResult<T>(data, mensagem);

    public static RequestResult<T> BadRequest(string? mensagem = "Não foi possível realizar requisição.")
    => new RequestResult<T>(mensagem);

    public static RequestResult<T> EntidadeInvalida(string? mensagem = "Entidade não foi validada. Verifique os requesistos necessários.")
   => new RequestResult<T>(mensagem);

    public static RequestResult<T> FalhaCommitRepository(string? mensagem = "Falha ao tentar realizar tarefa no banco de dados.")
   => new RequestResult<T>(mensagem);

    public static RequestResult<T> BadRequest(T data, string? mensagem = "Não foi possível realizar requisição.")
   => new RequestResult<T>(mensagem);
}
