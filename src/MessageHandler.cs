namespace MinecraftRcon;

public class MessageHandler
{
    public MessageHandler(int length, int id, MessageType type, string body)
    {
        Length = length;
        Id = id;
        Type = type;
        Body = body;
    }

    public int Length { get; }
    public int Id { get; }
    public MessageType Type { get; }
    public string Body { get; }
}

public enum MessageType
{
    Response,
    _,
    Command,
    Authenticate
}