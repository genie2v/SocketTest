using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkStream ns = null;
            StreamWriter writer = null;
            StreamReader reader = null;
            TcpClient client = new TcpClient("localhost", 8000);

            string para1 = "P1";
            string para2 = "P2";
            string para3 = "P3";

            if (client.Connected) Console.WriteLine("서버에 연결되었습니다");

            ns = client.GetStream();
            writer = new StreamWriter(ns);
            reader = new StreamReader(ns);

            writer.WriteLine(para1);
            writer.Flush();
            Console.WriteLine(para1 + "를 전송합니다");
            string receive = reader.ReadLine();
            Console.WriteLine("서버: " + receive);

            writer.WriteLine(para2);
            writer.Flush();
            Console.WriteLine(para2 + "를 전송합니다");
            receive = reader.ReadLine();
            Console.WriteLine("서버: " + receive);

            writer.WriteLine(para3);
            writer.Flush();
            Console.WriteLine(para3 + "를 전송합니다");
            receive = reader.ReadLine();
            Console.WriteLine("서버에서 받은 메세지: " + receive);

            writer.Close();
            reader.Close();
            client.Close();
            ns.Close();
            Console.WriteLine("서버접속 종료");
        }
    }
}

