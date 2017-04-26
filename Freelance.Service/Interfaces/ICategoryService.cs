using System;
using System.Collections.Generic;
using Freelance.Service.ServicesModel;


namespace Freelance.Service.Interfaces
{
    public interface ICategoryService : IService<CategoryServiceModel>
    {
        IDictionary<Guid, string> Lookup();
    }
}
