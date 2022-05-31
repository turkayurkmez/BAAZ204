using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace TestFeatureManagement.Controllers
{
    public class BetaController : Controller
    {
        private readonly IFeatureManager featureManager;

        public BetaController(IFeatureManager featureManager)
        {
            this.featureManager =  featureManager;
        }

        [FeatureGate(MyFeatureFlags.Beta)]
        public IActionResult Index()
        {
            return View();
        }
    }
}