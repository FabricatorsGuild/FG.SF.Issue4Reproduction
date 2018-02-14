namespace StatelessService1.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.ServiceFabric.Services.Remoting;

    public interface IStatelessService1 : IService
    {
        Task<ReturnDataModel> DoItAsync(RequestDataModel request, string text);
    }
}