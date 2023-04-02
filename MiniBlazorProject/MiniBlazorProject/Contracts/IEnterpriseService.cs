using Microsoft.AspNetCore.Components;
using MiniBlazorProject.Models;

namespace MiniBlazorProject.Contracts;

public interface IEnterpriseService
{
    Task<List<Enterprise>> GetEnterprises(int pageSize, int page);
    Task<int> GetEnterprisesCount();
    Task<Enterprise> GetEnterpriseById(string enterpriseId);
    Task<List<Enterprise>> QueryEnterprises(string query, int pageSize, int page);
    Task CreateEnterprise(Enterprise Enterprise);
    Task UpdateEnterprise(Enterprise Enterprise);
    Task DeleteEnterprise(string enterpriseId);
}
