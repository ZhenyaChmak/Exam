using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Quiz
{
    public class Questions
    {
        public int Id;
        public string Question;
        public string Answer;

        public Questions() { }

        Random rnd = new Random();
        string WayQuestions = ConfigurationManager.AppSettings["WayQuestions"];
        string WayTop = ConfigurationManager.AppSettings["WayTop"];

        public void Test(string name)
        {
            var str = File.ReadAllText(WayQuestions);
            var q = JsonConvert.DeserializeObject<List<Questions>>(str);

            int Rating = 0;
            int NumberOfResponses = 0;
            string Name = name;
            int size = q.Count;
            int voproc = 1;

            while (size>0)
            {
                size--;
                int r = rnd.Next(size + 1);
                var temp = q[r];
                q[r] = q[size];
                q[size] = temp;
               
                Console.Write("Вопрос №" + voproc + ": ");
                Console.WriteLine("\"" + temp.Question + "\"");
                string EnteredResponse = "";
                Console.Write("Введите ответ: ");
                
                var t = new Thread(() =>
                {
                    Thread.MemoryBarrier();
                    EnteredResponse = Console.ReadLine();
                });

                t.Start();
                t.Join(7000);

                if (temp.Answer == EnteredResponse)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Верно");
                    Rating++;
                    NumberOfResponses++;
                }
                else if (temp.Answer != EnteredResponse)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не верно");
                    Rating--;
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                t.Abort();
                voproc++;
            }

            Top top = new Top();
            top.Create(Name, Rating, NumberOfResponses);

            result(NumberOfResponses, Rating, Name);
        }

        void result(int NumberOfResponses, int Rating, string Name)
        {
            XDocument xdoc = XDocument.Load(WayTop);
            IEnumerable<XElement> tracks = from t in xdoc.Root.Elements("Users")
                                           let time = int.Parse(t.Element("Rating").Value)
                                           orderby time descending
                                           select t;
            int position = 1;

            foreach (XElement a in tracks)
            {
                if (a.Element("Name").Value == Name & a.Element("Rating").Value==Rating.ToString() & a.Element("NumberOfResponses").Value == NumberOfResponses.ToString())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Position:{position}\t Name:{a.Element("Name").Value}\t Rating:{a.Element("Rating").Value}\t " +
                        $"NumberOfResponses:{a.Element("NumberOfResponses").Value}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
                position++;
            }
        }
    }
}
