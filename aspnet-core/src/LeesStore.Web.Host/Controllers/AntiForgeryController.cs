using Microsoft.AspNetCore.Antiforgery;
using LeesStore.Controllers;

namespace LeesStore.Web.Host.Controllers
{
    public class AntiForgeryController : LeesStoreControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
