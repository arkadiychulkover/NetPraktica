using System;
using System.Net;
using System.Net.Sockets;
namespace NetPraktica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //FirstTask();
            //SendSquare();
            //SendTime();
        }
        static void FirstTask()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(ipAddress, 4252));
            socket.Listen(10);
            Console.WriteLine("Server is listening on");

            while (true)
            {
                Socket clientSocket = socket.Accept();
                Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint.ToString()} at {DateTime.Now}");

                byte[] buffer = new byte[1024];
                int receivedBytes = clientSocket.Receive(buffer);
                string receivedText = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedBytes);

                if (receivedText.Trim().ToLower() == "date")
                {
                    byte[] textSnd = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.Date.ToString());
                    clientSocket.Send(textSnd);
                    Console.WriteLine($"Server sent {DateTime.Now.Date.ToString()} at {DateTime.Now} from {socket.LocalEndPoint.ToString()}");
                    clientSocket.Close();
                }
                else
                {
                    byte[] textSnd = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.TimeOfDay.ToString());
                    clientSocket.Send(textSnd);
                    Console.WriteLine($"Server sent {DateTime.Now.TimeOfDay.ToString()} at {DateTime.Now} from {socket.LocalEndPoint.ToString()}");
                    clientSocket.Close();
                }

            }
        }

        static void SendSquare()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(ipAddress, 4252));
            socket.Listen(10);
            Console.WriteLine("Server is listening on");

            while (true)
            {
                Socket clientSocket = socket.Accept();
                Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint.ToString()} at {DateTime.Now}");

                byte[] buffer = new byte[1024];
                int receivedBytes = clientSocket.Receive(buffer);
                string receivedText = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                int num = int.Parse(receivedText);

                int squared = num * num;
                byte[] textSnd = System.Text.Encoding.UTF8.GetBytes($"{squared}");
                clientSocket.Send(textSnd);
                Console.WriteLine($"Server sent {squared} at {DateTime.Now} from {socket.LocalEndPoint.ToString()}");
                clientSocket.Close();
            }
        }

        static void SendTime() 
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(ipAddress, 4252));
            socket.Listen(10);
            Console.WriteLine("Server is listening on");

            List<Socket> clients = new List<Socket>();
            object lockObject = new object();

            Thread Acepter = new Thread(() =>
            {
                while (true)
                {
                    Socket clientSocket = socket.Accept();
                    lock (lockObject)
                    {
                        clients.Add(clientSocket);
                    }
                    Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint.ToString()} at {DateTime.Now}");
                }
            });
            Acepter.IsBackground = true;
            Acepter.Start();

            while (true)
            {
                string now = DateTime.Now.ToString();
                lock (lockObject)
                {
                    for (int i = clients.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            clients[i].Send(System.Text.Encoding.UTF8.GetBytes(now));
                            Console.WriteLine($"Sent {now} to {clients[i].RemoteEndPoint}");
                        }
                        catch
                        {
                            Console.WriteLine($"Client {clients[i].RemoteEndPoint} disconnected");
                            clients[i].Close();
                            clients.RemoveAt(i);
                        }
                    }
                }
                Thread.Sleep(1000);
            }

        }

    }
}
