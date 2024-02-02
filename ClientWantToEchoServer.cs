using System.Text.Json;
using Fleck;
using lib;

namespace ws;

public class ClientWantToEchoServerDto : BaseDto
{
    public string messageContent { get; set; }
}

public class ClientWantsToEchoServer : BaseEventHandler<ClientWantToEchoServerDto>
{
    
    public override Task Handle(ClientWantToEchoServerDto dto, IWebSocketConnection socket)
    {
        var echo = new ServerEchoClient()
        {
            echoValue = "echo: " + dto.messageContent
        };
        
        var messageToClient = JsonSerializer.Serialize(echo);
        socket.Send(messageToClient);
        
        return Task.CompletedTask;
    }
    
    public class ServerEchoClient : BaseDto
    {
        
        public string echoValue { get; set; }
        
    }
}