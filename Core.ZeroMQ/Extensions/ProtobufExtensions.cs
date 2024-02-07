using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Core.ZeroMQ.Extensions;

public static class ProtobufExtensions
{
    public static byte[] SerializeToByteArrayProtobuf<T>(this T obj) where T : class
    {
        using var ms = new MemoryStream();

        ProtoBuf.Serializer.Serialize(ms, obj);
        return ms.ToArray();
    }
    public static T DeserializeFromByteArrayProtobuf<T>(this byte[] arr) where T : class
    {
        using var ms = new MemoryStream(arr);

        return ProtoBuf.Serializer.Deserialize<T>(ms);
    }
}
