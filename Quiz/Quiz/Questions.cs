using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz
{
    public class Questions
    {
        public int Id;
        public string Question;
        public string Answer;

        public Questions() { }
      /*  public Questions(int Id, string Question, string Answer)
        {
            this.Id = Id;
            this.Question = Question;
            this.Answer = Answer;
        }*/

        Random rnd = new Random();
        string WayQuestions = ConfigurationManager.AppSettings["WayQuestions"];

        public void Test(string name)
        {
            var str = File.ReadAllText(WayQuestions);
            var q = JsonConvert.DeserializeObject<List<Questions>>(str);

            int Rating = 0;
            int NumberOfResponses = 0;
            string Name = name;
            int size = q.Count;
            int voproc = 1;

           // int a = 10;
           // while(a>0)
            while (size>0)
            {
           //     a--;
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

            result(NumberOfResponses, Rating);
        }

        void result(int NumberOfResponses, int Rating)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nКоличество правильно отвеченных вопросов: {0} \nТекущий рейтинг: {1} \nПозиция в рейтинге: ", NumberOfResponses, Rating);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
