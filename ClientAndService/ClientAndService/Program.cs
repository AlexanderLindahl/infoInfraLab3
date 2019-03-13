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

                Console.WriteLine("vad vill du göra?");
                i = Int32.Parse(Console.ReadLine());
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
                        conServ.GetFilteredByID(Int32.Parse(Console.ReadLine()));
                        Console.WriteLine(conServ.Result);
                        break;
                    case 4:
                        Console.WriteLine("skriv in vilken nod du letar efter");
                        conServ.GetFilteredByNode(Console.ReadLine());
                        Console.WriteLine(conServ.Result);
                        break;
                    case 5:
                        Console.WriteLine("vilket ID vill du leta efter?");
                        int id = Int32.Parse(Console.ReadLine());
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
                        conServ.GetPrettyInfoPrint(info);
                        Console.WriteLine(conServ.Result);
                        break;
                    case 8:
                        FileBackup.SaveToFile(conServ.Result.ToString());
                        Console.WriteLine("Resultat sparat");
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



        }
    }
}
