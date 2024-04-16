using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SurRon.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
