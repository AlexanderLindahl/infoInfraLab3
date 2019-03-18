using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService;
using System.Net;
using System.Xml.Linq;


namespace Client
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConnectToInterchangeService conServ = new ConnectToInterchangeService();

            string SID = "";
            int i = 10;
            while(i != 0){
                Console.WriteLine("Hej och välkommen till vår service");
                Console.WriteLine("1. GetAll");
                Console.WriteLine("2. GetTestData");
                Console.WriteLine("3. GetFilteredByID");
                Console.WriteLine("4. GetFilteredByNode");
                Console.WriteLine("5. GetFilteredByIDAndNode");
                Console.WriteLine("6. GetFilteredByNodeValue");
                Console.WriteLine("7. Läs in ny fil");
                Console.WriteLine("8. Spara Resultat");
                Console.WriteLine("9. Avsluta");

                Console.WriteLine("vad vill du göra?"+ "\n");
                i = Int32.Parse(Console.ReadLine());
                if(i < 1 || i > 9)
                {
                    Console.WriteLine("du måste välja ett nummer ur listan");
                }
                switch(i)
                {
                
                    case 1: 
                        conServ.GetAll();
                        Console.WriteLine(conServ.Result);
                        break;
                    case 2:
                        conServ.GetTestData();
                        Console.WriteLine(conServ.Result);
                        break;
                    case 3:
                        Console.WriteLine("Skriv in vilket id du vill kolla efter");
                        SID = Console.ReadLine();

                        if(CheckID(SID) == true)
                        {
                        conServ.GetFilteredByID(Int32.Parse(SID));
                        Console.WriteLine(conServ.Result);
                        }
                        else
                        {
                            Console.WriteLine("Du måste skriva in ett giltigt ID-format");
                        }
                        
                        break;
                    case 4:
                        Console.WriteLine("skriv in vilken nod du letar efter");
                        conServ.GetFilteredByNode(Console.ReadLine());
                        Console.WriteLine(conServ.Result);
                        break;
                    case 5:
                        bool correctId = false;
                        
                        while ( correctId == false) {
                            Console.WriteLine("vilket ID vill du leta efter?");
                            SID = Console.ReadLine();
                            correctId = CheckID(SID);
                            if(correctId == false)
                            {
                                Console.WriteLine("Du måste skriva in ett giltigt ID-format");
                            }
                        }
                        int id = Int32.Parse(SID);
                        Console.WriteLine("vilken nod letar du efter?");
                        string node = Console.ReadLine();
                        conServ.GetFilteredByIDAndNode(id, node);
                        Console.WriteLine(conServ.Result);
                        break;
                    case 6:
                        Console.WriteLine("skriv in den node du letar efter");
                        string node2 = Console.ReadLine();
                        Console.WriteLine("skriv in det värdet du letar efter");
                        string nodeValue = Console.ReadLine();
                        conServ.GetFilteredByNodeValue(node2, nodeValue);
                        Console.WriteLine(conServ.Result);
                        break;
                    case 7:
                        XElement info = XElement.Parse(FileBackup.LoadFile());
                        
                        Console.WriteLine(PrettyInfoPrint(info));
                        break;
                    case 8:
                        if(conServ.Result != null)
                        {
                            FileBackup.SaveToFile(conServ.Result.ToString());
                            Console.WriteLine("Resultat sparat");

                        }
                        else
                        {
                            Console.WriteLine("du har ännu inte ett resultat att spara");
                        }
                        
                        break;
                    case 9:
                        Console.WriteLine("Har du sparat ditt resultat?" + "\n" + "1. Spara och avsluta" + "\n" + "2. Avsluta");
                        string svar = Console.ReadLine();

                        if (svar == "1")
                        {
                            FileBackup.SaveToFile(conServ.Result.ToString());
                            Console.WriteLine("Sparar och avslutar");
                            i = 0;
                        }
                        else if (svar == "2")
                        {
                            Console.WriteLine("Avslutar utan att spara");
                            i = 0;
                        }
                        else
                        {
                        }
                        break;
      
                }

            }
            string PrettyInfoPrint(XElement text)
            {
                string prettyString = "";
                int j = 0;
                foreach (XElement element in text.Descendants("Interchange"))
                {
                    j++;
                    string interchange = "Interchange nmbr: " + j;

                    var patient = from p in element.Descendants("StructuredPersonName")
                                  let namn = p.Element("FirstGivenName").Value + " " + p.Element("FamilyName").Value
                                  select namn;

                    var physician = (from p in element.Descendants("HealthcarePerson")
                                     select p.Element("Name").Value).GroupBy(x => x).Select(x => x.First());

                    var medicine = from m in element.Descendants("ManufacturedProductId")
                                   select m.Element("ProductId").Value;

                    var dosage = from d in element.Descendants("UnstructuredInstructionsForUse")
                                 select d.Element("UnstructuredDosageAdmin").Value;

                    XElement info = new XElement("Info",
                                        new XElement("Patient", patient),
                                        new XElement("Physician", physician),
                                        new XElement("Medicine", medicine),
                                        new XElement("Dosage", dosage));

                    // Alternativ för att returnera string (?)
                    prettyString = prettyString + interchange + "\n" + "Patient: " + info.Element("Patient").Value + "\n" + "Physician: " + info.Element("Physician").Value + "\n" + "Medicine: " + info.Element("Medicine").Value + "\n" + "Dosage: " + info.Element("Dosage").Value + "\n";

                }
                

                return prettyString;






            }
            bool CheckID(string id)
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
}
