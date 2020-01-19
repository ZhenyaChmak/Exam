using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class Users
    {
        public string Name { get; set;}
        public int Rating { get; set; }
        public int NumberOfResponses { get; set; }

        public Users() { }
        public Users(string Name)
        {
            this.Name = Name;
        }

          public Users(int Position, string Name, int Rating, int NumberOfResponses)
          {
            this.Name = Name;
            this.Rating = Rating;
            this.NumberOfResponses = NumberOfResponses;  
          }
    }
}
