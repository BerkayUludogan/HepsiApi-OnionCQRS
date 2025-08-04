using Microsoft.Extensions.DependencyInjection;
using HepsiApi.Application.Interfaces.AutoMapper;

namespace HepsiApi.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services) {
        services.AddSingleton<IMapper,AutoMapper.Mapper>();
        }
    }
}
