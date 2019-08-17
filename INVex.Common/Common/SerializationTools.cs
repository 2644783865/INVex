using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace INVex.Common.Common
{
    public static class SerializationTools
    {
        public static byte[] SerializeToBytes<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }

        public static void SerializeDict(Dictionary<string, object> dictionary, Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(dictionary.Count);
            foreach (KeyValuePair<string, object> kvp in dictionary)
            {
                writer.Write(kvp.Key);

                byte[] valueBytes = SerializeToBytes(kvp.Value);
                writer.Write(valueBytes.Length);
                writer.Write(valueBytes);
            }
            writer.Flush();
        }

        public static Dictionary<string, object> DeserializeDict(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            Dictionary<string, object> dictionary = new Dictionary<string, object>(count);
            for (int n = 0; n < count; n++)
            {
                string key = reader.ReadString();

                int bytesCount = reader.ReadInt32();
                object value = DeserializeFromBytes(reader.ReadBytes(bytesCount));
                dictionary.Add(key, value);
            }
            return dictionary;
        }

        public static object DeserializeFromBytes(byte[] bytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return formatter.Deserialize(stream);
            }
        }
    }
}
