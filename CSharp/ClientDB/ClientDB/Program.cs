using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Net.Sockets;
using System.IO;

namespace ClientDB
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("localhost", 8000);
            if (client.Connected) Console.WriteLine("서버에 연결되었습니다");

            NetworkStream ns = client.GetStream();
            StreamWriter writer = new StreamWriter(ns);
            StreamReader reader = new StreamReader(ns);

            string strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)"
                + "(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));"
                + "User Id=person1;password=person;";

            OracleConnection conn = new OracleConnection(strConn);
            conn.Open();
            Console.WriteLine("DB연결");

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT A FROM TESTA";
            OracleDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read()) 
            {
                String para = rdr[0].ToString();
                writer.WriteLine(para);
                writer.Flush();
                Console.WriteLine(para + "를 전송합니다.");
                String receive = reader.ReadLine();
                Console.WriteLine("서버: " + receive);
            }

            writer.Close();
            reader.Close();
            client.Close();
            ns.Close();
            conn.Close();
            Console.WriteLine("서버접속 종료");
        }
    }
}
