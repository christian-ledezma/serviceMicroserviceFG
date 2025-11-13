using serviceMicroservice.Domain.Entities;

namespace serviceMicroservice.Domain.Ports;

public interface IServiceRepository
{
    public Task<IEnumerable<Service>> GetAllAsync();
    public Task<Service?> GetByIdAsync(int id);
    public Task<bool> CreateAsync(Service service, int userId);
    public Task<bool> UpdateAsync(Service service, int userId);
    public Task<bool> DeleteByIdAsync(int id, int userId);
}   