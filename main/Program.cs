using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MirrorAPI;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese auth token");
            string authToken = Console.ReadLine();
            Console.WriteLine("Ingrese mail");
            string email = Console.ReadLine();

            Service.GetInstance().SendCardWithActions(authToken, email);
        }
    }
}
