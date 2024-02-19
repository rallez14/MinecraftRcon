using System.Net.Sockets;

namespace MinecraftRcon;

internal class Client : IDisposable
{
    private const int MaxMessageSize = 4110; // 4096 + 14 header bytes
    private readonly TcpClient _client;
    private readonly NetworkStream _conn;
    private int _lastId;

    public Client(string host, int port)
    {
        _client = new TcpClient(host, port);
        _conn = _client.GetStream();
    }

    public void Dispose()
    {
        Close();
    }

    private void Close()
    {
        _conn.Close();
        _client.Close();
    }

    public async Task<bool> AuthenticateAsync(string password)
    {
        return await SendMessageAsync(new MessageHandler(
            password.Length + Encoder.HeaderLength,
            Interlocked.Increment(ref _lastId),
            MessageType.Authenticate,
            password
        ));
    }

    public async Task<MessageHandler> SendCommandAsync(string command)
    {
        if (await SendMessageAsync(new MessageHandler(
                command.Length + Encoder.HeaderLength,
                Interlocked.Increment(ref _lastId),
                MessageType.Command,
                command
            )))
            // Assuming the response is a success, create a dummy response
            return new MessageHandler(0, 0, MessageType.Response, "");
        // Handle the case where the command failed
        return null;
    }

    private async Task<bool> SendMessageAsync(MessageHandler req)
    {
        var encoded = Encoder.EncodeMessage(req);
        await _conn.WriteAsync(encoded, 0, encoded.Length);

        var respBytes = new byte[MaxMessageSize];
        var bytesRead = await _conn.ReadAsync(respBytes, 0, respBytes.Length);
        Array.Resize(ref respBytes, bytesRead);

        var resp = Encoder.DecodeMessage(respBytes);
        return req.Id == resp.Id;
    }
}