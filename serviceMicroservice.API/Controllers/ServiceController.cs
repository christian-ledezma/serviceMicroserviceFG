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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _repository.GetById(id);
        return Ok(res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Domain.Entities.Service service, [FromHeader(Name = "User-Id")] int userId)
    {
        service.Id = id;
        var res = await _repository.Update(service, userId);
        return Ok(res);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id, [FromHeader(Name = "User-Id")] int userId)
    {
        var success = await _repository.DeleteById(id, userId);

        if (!success)
            return NotFound(new { message = "Servicio no encontrado" });

        return Ok(new { message = "Servicio desactivado exitosamente" });
     }

}