using System.Text;
using System.Net.WebSockets;

namespace PokemonMasterminds.Model
{
    public class LobbyWebSocketClient
    {
        private ClientWebSocket webSocket;

        public LobbyWebSocketClient()
        {
            webSocket = new ClientWebSocket();
        }

        public WebSocketState State => webSocket.State;

        public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (webSocket.State == WebSocketState.Closed || webSocket.State == WebSocketState.None)
            {
                await webSocket.ConnectAsync(uri, cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("WebSocket is already connected.");
            }
        }

        private async Task ReceiveLoopAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[1024];
            while (!cancellationToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    // Handle the received message
                }
            }
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken);
        }

        public async Task DisconnectAsync()
        {
            if (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.CloseReceived)
            {
                await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
            webSocket.Dispose();
        }
    }
}
