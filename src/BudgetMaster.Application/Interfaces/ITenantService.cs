using System.Threading.Tasks;

namespace BudgetMaster.Application.Interfaces
{
    public interface ITenantService
    {
        Task CreateTenantAsync(string tenantId);
    }
}
