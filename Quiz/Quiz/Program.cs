using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace Quiz
{
    class Program
    {
        static void Main(string[] args)
        {
                Console.WriteLine("\t\t*** ВИКТОРИНА ***\n");

            Console.Write(Resource1.Name);
            string name = Console.ReadLine();
            /*Console.WriteLine("\n*********************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\t\t\tName: {name}");
            Console.WriteLine("\t***MENU***");
            Console.WriteLine("  1.Start \n  2.Past results \n  3.Top 20 \n  4.Exit");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("**********************************");*/
            Menu menu = new Menu();
            menu.PrintMenu(name);

            while (true)
            {
                int a;
                Console.Write("\nВыберите пункт из меню: ");
                int.TryParse(Console.ReadLine(), out a);
                switch (a)
                {
                    case 1:
                        Console.WriteLine("Start");
                        Questions questions = new Questions();
                        questions.Test(name);
                        break;
                    case 2:
                        Console.WriteLine("Past result:");
                        Past_results past = new Past_results(name);
                        past.Read();
                        break;
                    case 3:
                        Console.WriteLine("Top 20:");
                        Top top = new Top();
                        top.Read();
                        break;
                    case 4:
                        Console.Write(Resource1.Name);
                        name = Console.ReadLine();
                        menu.PrintMenu(name);
                        break;
                    case 5:
                        bool ExitCase = false;
                        while (true)
                        {
                            Console.WriteLine(" Y/N ?");

                            string vubor = Console.ReadLine();
                            switch (vubor)
                            {
                                case "Y":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Goodbye");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    return;
                                case "N":
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Back");
                                    ExitCase = true;
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(Resource1.Povtor);
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;
                            }

                            if (ExitCase)
                            {
                                break;
                            }
                        }
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Resource1.Povtor);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                }
            }
        }
    }
}

