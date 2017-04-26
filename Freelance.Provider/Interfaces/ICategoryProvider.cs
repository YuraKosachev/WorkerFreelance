using System;
using System.Collections.Generic;
using Freelance.Provider.EntityModels;

namespace Freelance.Provider.Interfaces
{
    public interface ICategoryProvider : IProvider<Category>
    {
        IDictionary<Guid, string> Lookup();
    }
}
