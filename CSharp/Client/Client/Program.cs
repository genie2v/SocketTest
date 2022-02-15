using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkStream ns = null;
            StreamWriter writer = null;
            StreamReader reader = null;
            TcpClient client = new TcpClient("localhost", 8000);

            string para1 = string.Empty;

            if (client.Connected) Console.WriteLine("서버에 연결되었습니다");

            while (true)
            {
                para1 = Console.ReadLine();
                
                ns = client.GetStream();
                writer = new StreamWriter(ns);
                reader = new StreamReader(ns);
                writer.WriteLine(para1);
                writer.Flush(); // message전송

                if (para1.Equals("done")) break;

                Console.WriteLine(para1 + "를 전송합니다");

                string msg1 = reader.ReadLine(); // server에서 리턴한 메시지를 받는다
                Console.WriteLine("서버에서 받은 메세지:" + msg1);
            }

            writer.Close();
            reader.Close();
            client.Close();
            Console.WriteLine("서버접속 종료");
        }
    }
}

