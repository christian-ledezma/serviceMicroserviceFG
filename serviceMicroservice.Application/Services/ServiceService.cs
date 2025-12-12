using serviceMicroservice.Domain.Entities;
using serviceMicroservice.Domain.Ports;

namespace serviceMicroservice.Application.Services;

public class ServiceService
{
    private readonly IServiceRepository _repository;
    
    public ServiceService(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Service>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Service?> GetById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<bool> Create(Service service, int userId)
    {
        return await _repository.CreateAsync(service, userId);
    }

    public async Task<bool> Update(Service service, int userId)
    {
        return await _repository.UpdateAsync(service, userId);
    }

    public async Task<bool> DeleteById(int id, int userId)
    {
        return await _repository.DeleteByIdAsync(id, userId);
    }
    
    public async Task<bool> UpdateAccumulatedRevenue(int serviceId, decimal amount, string operation)
    {
        if (operation != "ADD" && operation != "SUBTRACT")
        {
            throw new ArgumentException("La operación debe ser 'ADD' o 'SUBTRACT'", nameof(operation));
        }
        
        return await _repository.UpdateAccumulatedRevenueAsync(serviceId, amount, operation);
    }
}