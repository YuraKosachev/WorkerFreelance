using Microsoft.Owin;

namespace Freelance.Service.Interfaces.AuthServices
{
    public interface IManagerService
    {
        IOwinContext Context { get; set; }
    }
}
