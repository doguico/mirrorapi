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

            //Stream videoStream = new MemoryStream();
            //using (Stream video = File.OpenRead("C:\\Users\\Guido\\Desktop\\Vídeo14.wmv"))
            //{
            //    byte[] buffer = new byte[4096];

            //    while (true)
            //    {
            //        int bytesRead = video.Read(buffer, 0, buffer.Length);

            //        if (bytesRead == 0) break;
            //        videoStream.Write(buffer, 0, bytesRead);
            //    }
            //    videoStream.Position = 0;
            //}
            
            //Service.GetInstance().SendAttachment(authToken, email, "video/wmv", videoStream);           
            Service.GetInstance().SendPinCard(email);
        }
    }
}
