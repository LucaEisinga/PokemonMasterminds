using Microsoft.Maui.Controls;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiplayerLobby
{
    public class MainPage : ContentPage
    {
        private ClientWebSocket ws;
        private CancellationTokenSource tokenSource;

        public MainPage()
        {
            var stackLayout = new StackLayout
            {
                Children = {
                    new Label { Text = "Server" }
                }
            };

            Content = stackLayout;

            _ = StartWebSocketAsync();
        }

        async Task StartWebSocketAsync()
        {
            ws = new ClientWebSocket();
            tokenSource = new CancellationTokenSource();

            try
            {
                await ws.ConnectAsync(new Uri("ws://localhost"), tokenSource.Token);

                await Task.Run(async () =>
                {
                    while (!tokenSource.Token.IsCancellationRequested)
                    {
                        await ReadMessage();
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket connection error: {ex.Message}");
            }
        }

        async Task ReadMessage()
        {
            WebSocketReceiveResult result;
            var message = new ArraySegment<byte>(new byte[4096]);

            try
            {
                do
                {
                    result = await ws.ReceiveAsync(message, tokenSource.Token);

                    if (result.MessageType != WebSocketMessageType.Text)
                        break;

                    var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                    string receivedMessage = Encoding.UTF8.GetString(messageBytes);
                    // Process the received message as needed
                }
                while (!result.EndOfMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading WebSocket message: {ex.Message}");
            }
        }

        async Task SendMessageAsync(string message)
        {
            try
            {
                var byteMessage = Encoding.UTF8.GetBytes(message);
                var segment = new ArraySegment<byte>(byteMessage);
                await ws.SendAsync(segment, WebSocketMessageType.Text, true, tokenSource.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending WebSocket message: {ex.Message}");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DisconnectWebSocket();
        }

        void DisconnectWebSocket()
        {
            if (ws != null && ws.State == WebSocketState.Open)
            {
                tokenSource.Cancel();
                ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None).Wait();
                ws.Dispose();
                ws = null;
            }
        }
    }

    public class App : Application
    {
        protected override Window CreateWindow(IActivationState activationState)
        {
            var page = new MainPage();
            var window = new Window(page);
            return window;
        }
    }
}