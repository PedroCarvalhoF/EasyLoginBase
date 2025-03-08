namespace EasyLoginBase.InfrastructureData.Constants;

public static class Roles
{
    private static List<string> _funcoes = ["Programador","Admin","RH","Financeiro","SupervisorOperacional","Caixa","PermissaoSistema"];
    public static List<string> GetListaFuncoes()
    {
        return _funcoes;
    }

    public const string Programador = nameof(Programador);
    public const string Admin = nameof(Admin);
    public const string Supervisor = nameof(Supervisor);
    public const string Gerente = nameof(Gerente);
    public const string OperadorCaixa = nameof(OperadorCaixa);
}
