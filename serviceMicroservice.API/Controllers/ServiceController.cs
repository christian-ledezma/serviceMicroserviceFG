using Microsoft.AspNetCore.Mvc;
using serviceMicroservice.Application.Services;
using serviceMicroservice.Domain.Services;
using serviceMicroservice.DTOs;

namespace serviceMicroservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly ServiceService _serviceService;
    private readonly IValidator<Domain.Entities.Service> _validator;

    public ServiceController(ServiceService service, IValidator<Domain.Entities.Service> validator)
    {
        _serviceService = service;
        _validator = validator;
    }

    [HttpPost("insert")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Insert([FromBody] CreateServiceDto serviceDto, [FromHeader(Name = "User-Id")] int userId)
    {
        var service = new Domain.Entities.Service
        {
            Name = serviceDto.Name,
            Type = serviceDto.Type,
            Description = serviceDto.Description,
            Price = serviceDto.Price,
        };
        
        var validationResult = _validator.Validate(service);
        
        if (validationResult.IsFailure)
        {
            return BadRequest(new ValidationErrorResponse
            {
                Message = "Validación fallida",
                Errors = validationResult.Errors
            });
        }
        
        var created = await _serviceService.Create(service, userId);

        if (!created)
        {
            return StatusCode(500, new { message = "Error al crear el servicio" });
        }

        return CreatedAtAction(
            nameof(GetById), 
            new { id = service.Id }, 
            new SuccessResponse
            {
                Message = "Servicio creado exitosamente",
                Id = service.Id
            });
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Domain.Entities.Service>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Select()
    {
        var services = await _serviceService.GetAll();
        return Ok(services);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Domain.Entities.Service), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    { 
        var service = await _serviceService.GetById(id);

        if (service == null)
        {
            return NotFound(new { message = $"Servicio con ID {id} no encontrado" });
        }

        return Ok(service);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] Domain.Entities.Service service, [FromHeader(Name = "User-Id")] int userId)
    {
        var existingService = await _serviceService.GetById(id);
        if (existingService == null)
        {
            return NotFound(new { message = $"Servicio con ID {id} no encontrado" });
        }

        service.Id = id;

        var validationResult = _validator.Validate(service);
        
        if (validationResult.IsFailure)
        {
            return BadRequest(new ValidationErrorResponse
            {
                Message = "Validación fallida",
                Errors = validationResult.Errors
            });
        }

        var success = await _serviceService.Update(service, userId);

        if (!success)
        {
            return StatusCode(500, new { message = "Error al actualizar el servicio" });
        }

        return Ok(new SuccessResponse
        {
            Message = "Servicio actualizado exitosamente",
            Id = id
        });
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(int id, [FromHeader(Name = "User-Id")] int userId)
    {
        var existingService = await _serviceService.GetById(id);
        if (existingService == null)
        {
            return NotFound(new { message = $"Servicio con ID {id} no encontrado" });
        }

        var success = await _serviceService.DeleteById(id, userId);

        if (!success)
        {
            return NotFound(new { message = "Servicio no encontrado o ya está inactivo" });
        }

        return Ok(new { message = "Servicio desactivado exitosamente" });
    
     }

}