using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.AppLogger
{
    public interface ILogger
    {
        IEnumerable<XElement> List();
        void Add(XElement element);
        XElement Item(Guid id);
        void Remove(Guid id);
    }
}
