using System.Reflection;
using Fleck;
using lib;


var builder = WebApplication.CreateBuilder(args);

var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

var app = builder.Build();

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
        app.InvokeClientEventHandler(clientEventHandlers, ws, message);
    };
} );

Console.ReadLine();

