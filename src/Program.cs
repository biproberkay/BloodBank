using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Temel Programlama Ödevi Group5";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WindowWidth = 68;
            Console.WindowHeight = 48;
            BloodBank bloodBank = new BloodBank();
            while (bloodBank.AppUser==null)
            {
                bloodBank.GuestMenu();
            }
            while (bloodBank.AppUser != null)
            {
                bloodBank.Menu();
            }
        }

    }
}
