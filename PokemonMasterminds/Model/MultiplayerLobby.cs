using Microsoft.Maui.Controls;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerLobby
{
    public class MainPage : ContentPage
    {
        private HttpListener httpListener;

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

        private async void StartServer()
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:8888/");
            httpListener.Start();
            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                HttpListenerContext context = await httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    Console.WriteLine("WebSocket client connected!");

                    // Handle the WebSocket client connection asynchronously
                    HandleClient(webSocketContext);
                }
            }
        }

        private async void HandleClient(HttpListenerWebSocketContext webSocketContext)
        {
            WebSocket webSocket = webSocketContext.WebSocket;

            // Example: Send a message to the client
            string message = "Welcome to the server!";
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

            // Example: Receive messages from the client
            byte[] receiveBuffer = new byte[1024];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                Console.WriteLine("Received message from client: " + receivedMessage);

                // Example: Send a response to the client
                string responseMessage = "Server received: " + receivedMessage;
                byte[] responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
                await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);

                // Receive the next message
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            }

            // Close the WebSocket connection
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            Console.WriteLine("WebSocket client disconnected!");
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