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

            Console.WriteLine("Ultima location");
            Service.GetInstance().GetLastLocation(authToken, email);
            Console.WriteLine("******");
            Console.WriteLine("Ultimas locations");
            Service.GetInstance().GetAllLocations(authToken, email);
            Console.ReadLine();
        }
    }
}
