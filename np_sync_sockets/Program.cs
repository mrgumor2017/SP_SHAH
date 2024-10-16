using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace np_sync_sockets
{
    class Program
    {
        static string address = "192.168.31.160"; // поточний адрес
        static int port = 8080;              // порт для приема входящих запросов

        static void Main(string[] args)
        {
            // словник для відповідей
            Dictionary<string, string> responses = new Dictionary<string, string>()
            {
                { "hello", "Hi!"},
                { "hi", "Hi!"},
                { "who are you?", "I'm a server!" },
                { "how are you?", "I'm fine" },
                { "bye", "Goodbye!" }
            };

            // отримуємо адреса для запуску сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // створюємо сокет
            UdpClient listener = new UdpClient(ipPoint);

            try
            {
                Console.WriteLine("Server started! Waiting for connection...");

                while (true)
                {
                    // отримуємо повідомлення
                    byte[] data = listener.Receive(ref remoteEndPoint);
                    string msg = Encoding.Unicode.GetString(data);
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");

                    string response;
                    if (responses.TryGetValue(msg.ToLower(), out response))
                    {
                        data = Encoding.Unicode.GetBytes(response);
                    }
                    else
                    {
                        response = "I don't understand your question.";
                        data = Encoding.Unicode.GetBytes(response);
                    }

                    listener.Send(data, data.Length, remoteEndPoint);
                    Console.WriteLine($"Response sent: {response}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}
