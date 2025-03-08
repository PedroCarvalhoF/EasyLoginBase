using EasyLoginBase.InfrastructureData.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EasyLoginBase.InfrastructureData.Context;

public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {

        string DefaultConnectionDESENVOLVIMENTO =
            $"Server={ConfiguracaoBanco.Server};" +
            $"Port={ConfiguracaoBanco.Port};" +
            $"DataBase={ConfiguracaoBanco.DataBase};" +
            $"Uid={ConfiguracaoBanco.Uid};" +
            $"password={ConfiguracaoBanco.Password};";

        DbContextOptionsBuilder<MyContext> optionsBuilder = new DbContextOptionsBuilder<MyContext>();
        optionsBuilder.UseMySql(DefaultConnectionDESENVOLVIMENTO, new MySqlServerVersion(new Version(8, 0, 21)));

        return new MyContext(optionsBuilder.Options);
    }
}
