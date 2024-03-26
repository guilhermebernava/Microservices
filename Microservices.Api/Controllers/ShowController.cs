using Microservices.Infra.UnityOfWok;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShowController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllData([FromServices] IUnityOfWork unityOfWork)
    {
        var data = await unityOfWork.RecomendationRepository.GetAllAsync();
        return Ok(data);
    }
}
