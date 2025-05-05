namespace EasyLoginBase.InfrastructureData.Constants;

public static class Roles
{
    //public const string Programador = nameof(Programador);
    //public const string Admin = nameof(Admin);
    //public const string Supervisor = nameof(Supervisor);
    //public const string Gerente = nameof(Gerente);
    //public const string OperadorCaixa = nameof(OperadorCaixa);

    private static List<string> _funcoes = ["Programador", "Admin", "Supervisor", "Auxiliar Cozinha", "Cozinheiro(a)", "Copeiro(a)"];
    public static List<string> GetListaFuncoes()
    {
        return _funcoes;
    }
}
