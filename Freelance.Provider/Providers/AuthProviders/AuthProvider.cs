using System;
using Freelance.Provider.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Freelance.Provider.Providers
{



    public abstract class AuthProvider<TManager> : IManagerProvider, IDisposable
        where TManager : class, IDisposable
    {
        protected TManager _manager;
        public IOwinContext Context { get; set; }

        public AuthProvider() { }

        public AuthProvider(IOwinContext context, TManager manager)
        {
            Manager = manager;
            Context = context;
        }
        public TManager Manager
        {
            get
            {
                return _manager ?? Context.Get<TManager>();
            }
            protected set
            {
                _manager = value;
            }
        }





        private bool disposed = false;
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_manager != null)
                {
                    _manager.Dispose();
                    _manager = null;
                }


            }

            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
