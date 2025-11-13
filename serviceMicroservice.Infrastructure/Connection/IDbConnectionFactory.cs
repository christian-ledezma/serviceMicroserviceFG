using System.Data.Common;

namespace serviceMicroservice.Infrastructure.Connection;

public interface IDbConnectionFactory
{
    DbConnection  CreateConnection();

    string GetProviderName();
}