using BudgetMaster.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BudgetController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BudgetController(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        var tenantId = HttpContext.Items["TenantId"]?.ToString();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new InvalidOperationException("Tenant ID is missing.");
        }

        _context = dbContextFactory.CreateDbContext();
        _context.SetTenantId(tenantId);
    }

    // Actions...
}