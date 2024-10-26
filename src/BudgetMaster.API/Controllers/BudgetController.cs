using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BudgetMaster.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        // Actions...

        //private readonly UserManager<ApplicationUser> _userManager;

       // [Authorize]
       // [HttpGet("budgets")]
        //public async Task<IActionResult> GetBudgets()
       // {
            //var tenantId = HttpContext.Items["TenantId"].ToString();
            // Retrieve budgets for the tenant
            //var budgets = await _budgetService.GetBudgetsForTenantAsync(tenantId);

         //   return Ok(/*budgets*/);
        //}
    }
}