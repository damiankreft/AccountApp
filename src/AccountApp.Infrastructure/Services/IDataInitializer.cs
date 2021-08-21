using System.Threading.Tasks;

namespace AccountApp.Infrastructure.Services
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}