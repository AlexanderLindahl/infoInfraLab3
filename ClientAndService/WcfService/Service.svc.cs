using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {

        static XElement _testData;
        static XElement _interchanges;
        static XElement _ics;

        public Service()
        {
            using (WebClient wc = new WebClient())
            {
               string ics = wc.DownloadString(Encoding.UTF8.GetString(
                    Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vaWNzLmpzb24 =")));

                string test = wc.DownloadString(Encoding.UTF8.GetString(
                    Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vdGVzdERhdGEuanNvbg ==")));

                _interchanges = JsonConvert.DeserializeObject<XElement>(ics);

                _testData = JsonConvert.DeserializeObject<XElement>(test);

                _ics = _testData;

            }

            
            
        }
        
        
        public XElement FilterByInterchangeID(int id)
        {
            XElement result =
                              new XElement("Interchanges",
                              from interchange in _ics.Descendants("Interchange")
                              where interchange.Element("MessageRoutingAddress").Element("InterchangeRef").Value == id.ToString()
                              select interchange);

            return result;
                              
        }

        public XElement FilterByInterchangeIDAndNode(int id, string node)
        {
            XElement result = new XElement(node,
                                    from n in FilterByInterchangeID(id).Descendants(node).Take(1)
                                    select n.Value);
                               

            return result;
        }

        public XElement FilterByInterchangeNode(string node)
        {
            XElement result = new XElement(node + "s",
                                from interchange in _ics.Descendants("Interchange")
                                select interchange.Descendants(node)
                                    );

            return result;
        }

        public XElement FilterByInterchangeNodeAndValue(string node, string value)
        {
            var result = new XElement(node+"s",
                                from interchange in _ics.Descendants("Interchange")
                                where interchange.Descendants(node).Any(n => n.Value == value) 
                                select interchange);

            return result;
        }

        public XElement GetAllInterchanges()
        {
            return _interchanges;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public XElement GetTestData()
        {
           return _testData;
        }
    }
}
