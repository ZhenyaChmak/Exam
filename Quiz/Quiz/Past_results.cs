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

        public void Read()
        {
            try
            {
                XDocument xdoc = XDocument.Load(WayTop);
                IEnumerable<XElement> tracks = from t in xdoc.Root.Elements("Users")
                                               let time = int.Parse(t.Element("Rating").Value)
                                               orderby time descending
                                               select t;
                int position = 1;
                bool count = true;

                foreach (XElement a in tracks)
                {
                    if (a.Element("Name").Value == Name)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Position:{position}\t Name:{a.Element("Name").Value}\t Rating:{a.Element("Rating").Value}\t " +
                            $"NumberOfResponses:{a.Element("NumberOfResponses").Value}");
                        count = false;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    position++;
                }
                    if (count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Resource1.Absence);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
            }

            catch
            {
                Console.WriteLine(Resource1.Exception);
            }
        }
    }
}
