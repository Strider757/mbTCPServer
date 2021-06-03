using EasyModbus;
using System;
using System.Net;
using System.Net.Sockets;

namespace mbTCPserver
{
    class Program
    {
        static void Main(string[] args)
        {
            ModbusServer mbServer = new ModbusServer();
            ModbusClient mbClient = new ModbusClient();
            mbServer.Port = 502;
            mbClient.Port = 502;

            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    mbServer.LocalIPAddress = ip;
                    mbClient.IPAddress = ip.ToString();
                    break;
                }
            }
            mbServer.Listen();
            Console.WriteLine("Server loaded: " + mbServer.LocalIPAddress);

            mbClient.Connect();
            
            while(true)
            {
                Console.Write("register value: ");
                string[] s = Console.ReadLine().Split(' ');
                mbClient.WriteSingleRegister(int.Parse(s[0]), int.Parse(s[1]));
            }
        }



    }
}
