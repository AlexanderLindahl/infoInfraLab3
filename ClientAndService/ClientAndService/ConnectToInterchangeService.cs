using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WcfService;

namespace Client
{
    public class ConnectToInterchangeService
    {
        Service service = new Service();

        private XElement _Result;
        public XElement Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
                _Result.Add(new XAttribute("Datum&Tid", DateTime.Now.Minute));
                
            }
        }

        public void GetAll()
        {
            _Result = service.GetAllInterchanges();
        }
        public void GetTestData()
        {
            _Result = service.GetTestData();
        }
        public void GetFilteredByID(int id)
        {
            _Result = service.FilterByInterchangeID(id);
        }
        public void GetFilteredByNode(string node)
        {
            _Result = service.FilterByInterchangeNode(node);
        }
        public void GetFilteredByIDAndNode(int id, string node)
        {
            _Result = service.FilterByInterchangeIDAndNode(id, node);
        }
        public void GetFilteredByNodeValue(string node, string nodeValue)
        {
            _Result = service.FilterByInterchangeNodeAndValue(node, nodeValue);
        }
        public string GetPrettyInfoPrint(XElement text)
        {
            
            return  service.PrettyInfoPrint(text);
            
        }
        public bool CheckID(string id)
        {
            foreach (char c in id)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }




    }
}
