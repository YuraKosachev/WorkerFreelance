using Microsoft.Owin;

namespace Freelance.Provider.Interfaces
{
    public interface IManagerProvider
    {
        IOwinContext Context { get; set; }
    }
}
