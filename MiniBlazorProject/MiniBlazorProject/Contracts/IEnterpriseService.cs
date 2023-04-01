using MiniBlazorProject.Models;

namespace MiniBlazorProject.Contracts
{
    public interface IEnterpriseService
    {
        Task<List<Enterprise>> GetEnterprises(int pageSize, int page);
        Task<Enterprise> GetEnterpriseById(string EnterpriseId);
        Task<List<Enterprise>> QueryEnterprises(string query);
        Task CreateEnterprise(Enterprise Enterprise);
        Task UpdateEnterprise(Enterprise Enterprise);
        Task DeleteEnterprise(string EnterpriseId);
    }
}
