using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Quiz
{
   public class Past_results 
    {
        string Name;

        public Past_results(string Name)
        {
            this.Name = Name;
        }
        
        string WayTop = ConfigurationManager.AppSettings["WayTop"];
        XmlSerializer formatter = new XmlSerializer(typeof(List<Users>));
        List<Users> users;

        public void Read()
        {
            try
            {
                /*XDocument xdoc = XDocument.Load(WayTop);
                IEnumerable<XElement> tracks = from t in xdoc.Root.Elements("Users")
                                               let time = int.Parse(t.Element("Rating").Value)
                                               orderby time descending
                                               select t;
                int position = 1;
                bool cou = true;
                
                foreach (XElement a in tracks)
                {
                    if (a.Element("Name").Value == Name)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Position:{position}\t Name:{a.Element("Name").Value}\t Rating:{a.Element("Rating").Value}\t " +
                            $"NumberOfResponses:{a.Element("NumberOfResponses").Value}");
                        cou = false;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }


                if (cou)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Resource1.Absence);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }*/

                using (FileStream fs = new FileStream(WayTop, FileMode.OpenOrCreate))
                {
                    users = (List<Users>)formatter.Deserialize(fs);
                    bool count = true;
                    int con = 0;
                    foreach (Users i in users.OrderBy(c=>c.Rating))
                    {
                        con++;
                        if (i.Name == Name)
                        {
                          //  var index = users.FindIndex(c => c == i) + 1;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Position:{con}\t Name:{i.Name}\t Rating:{i.Rating}\t NumberOfResponses:{i.NumberOfResponses}");
                            count = false;
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }

                if (count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Resource1.Absence);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
            }

            catch
            {
                Console.WriteLine(Resource1.Exception);
            }
        }
    }
}
