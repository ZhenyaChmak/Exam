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
using KVPair = System.Collections.Generic.KeyValuePair<int, int>;

namespace Quiz
{
    public class Top
    {
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
                int count = 1;
                bool cou = true;

                foreach (XElement a in tracks)
                {
                    count++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Position:{position}\t Name:{a.Element("Name").Value}\t Rating:{a.Element("Rating").Value}\t " +
                        $"NumberOfResponses:{a.Element("NumberOfResponses").Value}");
                    cou = false;
                    Console.ForegroundColor = ConsoleColor.Gray;

                    if (count > 20)
                    {
                        break;
                    }
                    position++;
                }

                if (cou)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Resource1.Exception);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            catch
            {
                Console.WriteLine(Resource1.Exception);
            }
        }

        public void Create(string Name, int Rating, int NumberOfResponses)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(WayTop);
            XmlElement xRoot = xDoc.DocumentElement;
            // создаем новый элемент user
            XmlElement userElement = xDoc.CreateElement("Users");
            // создаем элементы 
            XmlElement name = xDoc.CreateElement("Name");
            XmlElement rating = xDoc.CreateElement("Rating");
            XmlElement numberOfResponses = xDoc.CreateElement("NumberOfResponses");
            // создаем значения
            XmlText nameText = xDoc.CreateTextNode(Name);
            XmlText ratingText = xDoc.CreateTextNode(Rating.ToString());
            XmlText numberOfResponsesText = xDoc.CreateTextNode(NumberOfResponses.ToString());

            //добавляем узлы
            name.AppendChild(nameText);
            rating.AppendChild(ratingText);
            numberOfResponses.AppendChild(numberOfResponsesText);

            userElement.AppendChild(name);
            userElement.AppendChild(rating);
            userElement.AppendChild(numberOfResponses);

            xRoot.AppendChild(userElement);
            xDoc.Save(WayTop);
        }
    }
}
