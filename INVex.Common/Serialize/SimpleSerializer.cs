using INVex.Common.Serialize.Base;
using System;
using System.Collections.Generic;
using System.IO;

namespace INVex.Common.Serialize
{
    public static class SimpleSerializer
    {

        private static Dictionary<string, Type> registereredTypes = new Dictionary<string, Type>();

        public static void RegisterType(string typeName, Type type)
        {
            if (type.GetInterface(typeof(IBinarySerializable).Name) == null)
            {
                throw new Exception("Попытка зарегистрировать несериализируемый тип.");
            }
            if (registereredTypes.ContainsKey(typeName))
            {
                throw new Exception("Попытка повторно зарегистрировать тип.");
            }
            registereredTypes.Add(typeName, type);
        }

        #region Serialization

        public static void PackObject(BinaryWriter writer, IBinarySerializable serializableObj)
        {
            if (serializableObj == null)
            {
                writer.Write(string.Empty);
                //throw new Exception("Попытка упаковать NULL");
            }
            else
            {
                serializableObj.Pack(writer);
            }
        }

        public static IBinarySerializable UnpackObject(BinaryReader reader)
        {
            string typeName = reader.ReadString();

            if (typeName.Length == 0)
            {
                return null;
            }

            Type findedType = null;
            if (registereredTypes.ContainsKey(typeName))
            {
                findedType = registereredTypes[typeName];
            }
            else
            {
                throw new Exception("Попытка распаковать неизвестный тип");
            }

            IBinarySerializable result = (IBinarySerializable)Activator.CreateInstance(findedType);
            result.Unpack(reader);
            return result;
        }

        #endregion
    }
}
