using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class Menu
    {
        public void PrintMenu(string Name)
        {
            Console.WriteLine("\n*********************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\t\t\tName: {Name}");
            Console.WriteLine("\t***MENU***");
            Console.WriteLine("  1.Start \n  2.Past results \n  3.Top 20 \n  4.Change user \n  5.Exit");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("**********************************");
        }
    }
}
