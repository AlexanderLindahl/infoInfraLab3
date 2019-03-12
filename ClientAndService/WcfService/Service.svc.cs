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
    

        public Service()
        {
            using (WebClient wc = new WebClient())
            {
               string ics = wc.DownloadString(Encoding.UTF32.GetString(
                    Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vaWNzLmpzb24 =")));

                string test = wc.DownloadString(Encoding.UTF32.GetString(
                    Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vdGVzdERhdGEuanNvbg ==")));

                _interchanges = JsonConvert.DeserializeObject<XElement>(ics);

                _testData = JsonConvert.DeserializeObject<XElement>(test);


            }

            
            
        }
        
        
        public XElement FilterByInterchangeID(int id)
        {
            throw new NotImplementedException();
        }

        public XElement FilterByInterchangeIDAndNode(int id, string node)
        {
            throw new NotImplementedException();
        }

        public XElement FilterByInterchangeNode(string node)
        {
            throw new NotImplementedException();
        }

        public XElement FilterByInterchangeNodeAndValue(string node, string value)
        {
            throw new NotImplementedException();
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
