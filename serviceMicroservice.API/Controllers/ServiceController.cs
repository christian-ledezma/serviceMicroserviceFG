using Microsoft.AspNetCore.Mvc;
using serviceMicroservice.Application.Services;

namespace serviceMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly ServiceService _repository;

    public 
        ServiceController(ServiceService service)
    {
        this._repository= service;
    }

    [HttpPost("insert")]
    public async Task<IActionResult> Insert([FromBody] Domain.Entities.Service service, [FromHeader(Name = "User-Id")] int userId)
    {
        var res = await _repository.Create(service, userId);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> Select()
    {
        var res = await _repository.GetAll();
        return Ok(res);
    }
}