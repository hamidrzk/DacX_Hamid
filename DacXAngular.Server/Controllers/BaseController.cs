using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI1.Controllers
{
	[Route("/api/[controller]")] // api/<controller>
  [ApiController]
  public class BaseApiController : ControllerBase
  {
     
  }
}
