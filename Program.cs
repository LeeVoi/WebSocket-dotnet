using Fleck;


var server = new WebSocketServer("ws://0.0.0.0:8181");

var wsConnection = new List<IWebSocketConnection>();

server.Start(ws =>
{
    ws.OnOpen = () =>
    {
        wsConnection.Add(ws);
    };
    ws.OnMessage = message =>
    {
        foreach (var webSocketConnection in wsConnection)
        {
            webSocketConnection.Send(message);
        }
    };
} );

WebApplication.CreateBuilder(args).Build().Run();

