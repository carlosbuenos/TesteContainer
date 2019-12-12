using Domain.Pessoas.Interfaces.Services;

namespace Domain.Pessoas.Interfaces.ServiceImplementation
{
    public class Service<T> : IService<T> where T : class
    {
    }
}
