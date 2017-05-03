using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.AppLogger
{
    public class XmlLogger : ILogger
    {
        private XDocument Doc { get; set; }
       
        public XmlLogger()
        {
            Doc = XDocument.Load(LoggerConf.LoggerFullFilePath);
        }
        public void Add(XElement element)
        {
            Doc.Root.Add(element);
            Doc.Save(LoggerConf.LoggerFullFilePath);
        }

        public XElement Item(Guid id)
        {
            return Doc.Element("exceptions").Elements("exception").Where(item => Guid.Parse(item.Attribute("id").Value) == id).First();
        }

        public IEnumerable<XElement> List()
        {
            return Doc.Element("exceptions").Elements("exception");
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
