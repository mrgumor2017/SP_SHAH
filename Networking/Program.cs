using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Networking
{
    class Program
    {
        // адрес і порт сервера, до якого будемо підключатися
        static string address = "192.168.31.160"; // адрес сервера
        static int port = 8080;             // порт сервера

        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);

                UdpClient client = new UdpClient();
                string message = "";

                while (message != "bye")
                {
                    Console.Write("Enter a message: ");
                    message = Console.ReadLine();

                    byte[] data = Encoding.Unicode.GetBytes(message);
                    client.Send(data, data.Length, ipPoint);

                    // отримуємо відповідь від сервера
                    data = client.Receive(ref remoteIpPoint);
                    string response = Encoding.Unicode.GetString(data);

                    Console.WriteLine("server response: " + response);
                }

                client.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
