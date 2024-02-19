using System.Text;

namespace MinecraftRcon;

public static class Encoder
{
    public const int HeaderLength = 10;

    public static byte[] EncodeMessage(MessageHandler msg)
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(msg.Length));
        bytes.AddRange(BitConverter.GetBytes(msg.Id));
        bytes.AddRange(BitConverter.GetBytes((int)msg.Type));
        bytes.AddRange(Encoding.ASCII.GetBytes(msg.Body));
        bytes.AddRange(new byte[] { 0, 0 });

        return bytes.ToArray();
    }

    public static MessageHandler DecodeMessage(byte[] bytes)
    {
        var len = BitConverter.ToInt32(bytes, 0);
        var id = BitConverter.ToInt32(bytes, 4);
        var type = BitConverter.ToInt32(bytes, 8);

        var bodyLen = bytes.Length - (HeaderLength + 4);
        if (bodyLen <= 0) return new MessageHandler(len, id, (MessageType)type, "");
        var bodyBytes = new byte[bodyLen];
        Array.Copy(bytes, 12, bodyBytes, 0, bodyLen);
        Array.Resize(ref bodyBytes, bodyLen);
        return new MessageHandler(len, id, (MessageType)type, Encoding.ASCII.GetString(bodyBytes));
    }
}