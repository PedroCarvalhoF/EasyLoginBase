using EasyLoginBase.Application.Dto.User.Role;
using EasyLoginBase.Services.Services.User.Roles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyLoginBase.CrossCutting.DependencyInjection.Extensions;
public static class RoleSeeder
{
    public static async Task SeedRolesAsync(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var userRoleServices = scope.ServiceProvider.GetRequiredService<IUserRoleServices<RoleDto, RoleUserDto>>();
        await userRoleServices.CriarRoles();
    }
}
