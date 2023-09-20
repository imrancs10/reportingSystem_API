using ReportingSystem.API.DTO.Request;
using ReportingSystem.API.DTO.Response;
using System.Threading.Tasks;

namespace ReportingSystem.API.Repositories.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(Guid Id);
        Task<T> Get(Guid Id);
    }
}
