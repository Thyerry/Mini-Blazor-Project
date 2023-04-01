using MiniBlazorProject.Models;

namespace MiniBlazorProject.Services
{
    public interface IEnterpriseService
    {
        Task<List<Enterprise>> GetEnterprises(int pageSize, int page);
        Task CreateEnterprise(Enterprise enterprise);
    }
}
