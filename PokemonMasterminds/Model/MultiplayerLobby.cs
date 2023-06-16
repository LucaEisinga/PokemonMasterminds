using Microsoft.Maui.Controls;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MultiplayerLobby
{
    public class MainPage : ContentPage
    {
        private TcpListener serverSocket;

        public MainPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Server" }
                }
            };

            StartServer();
        }

        private void StartServer()
        {
            serverSocket = new TcpListener(IPAddress.Any, 8888);
            serverSocket.Start();
            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                TcpClient clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Client connected!");

                // Handle the client connection in a separate thread or asynchronously
                HandleClient(clientSocket);
            }
        }

        private void HandleClient(TcpClient clientSocket)
        {
            // Handle client communication here
            NetworkStream stream = clientSocket.GetStream();

            // Example: Send a message to the client
            string message = "Welcome to the server!";
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);

            // Example: Receive a message from the client
            buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received message from client: " + message);

            // Close the client connection
            clientSocket.Close();
            Console.WriteLine("Client disconnected!");
        }
    }

    public class App : Application
    {
        public App()
        {
            MainPage = new MainPage();
        }
    }
}