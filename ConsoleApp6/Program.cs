﻿using BloodBank.ConsoleApp1.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Temel Programlama Ödevi Group5";
            Console.WindowWidth = 68;
            Console.WindowHeight = 48;
            BloodBank bloodBank = new BloodBank();
            while (true)
            {
                bloodBank.Menu();
            }
        }

    }
}