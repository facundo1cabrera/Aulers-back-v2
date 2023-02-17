using System.Data;

namespace AulersAPI.Infrastructure
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
