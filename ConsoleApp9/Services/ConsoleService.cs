using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9.Services
{
    internal class ConsoleService
    {
        public string takeAnswerString(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        public int takeAnswerInt(string text)
        {
            Console.WriteLine(text);
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
