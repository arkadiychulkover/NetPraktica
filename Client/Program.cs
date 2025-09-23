using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //FirstTask();
            //GetSquare();
            //GetTime();
        }

        static void FirstTask()
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(iPAddress, 4252));

            byte[] buffer = new byte[1024];
            byte[] textSnd = System.Text.Encoding.UTF8.GetBytes(Console.ReadLine());
            socket.Send(textSnd);
            int receivedBytes = socket.Receive(buffer);
            string receivedText = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            Console.WriteLine($"Received: {receivedText}");
            socket.Close();
            Console.ReadLine();
        }
        static void GetSquare()
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(iPAddress, 4252));

            byte[] buffer = new byte[1024];
            byte[] textSnd = System.Text.Encoding.UTF8.GetBytes(Console.ReadLine());
            socket.Send(textSnd);
            int receivedBytes = socket.Receive(buffer);
            string receivedText = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            Console.WriteLine($"Received: {receivedText}");
            socket.Close();
            Console.ReadLine();
        }
        static void GetTime() 
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(iPAddress, 4252));
            Console.WriteLine("socket conected");

            byte[] buffer = new byte[1024];
            while (true)
            {
                int receivedBytes = socket.Receive(buffer);
                string receivedText = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                Console.WriteLine($"{receivedText}");
                receivedBytes = 0;
            }
        }
    }
}

