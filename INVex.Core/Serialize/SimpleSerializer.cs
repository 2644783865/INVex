using INVex.Core.Objects;
using INVex.Core.Serialize.Base;

using System;
using System.Collections.Generic;
using System.IO;

namespace INVex.Core.Serialize
{
    public static class SimpleSerializer
    {

        private static Dictionary<string, Type> registereredTypes = new Dictionary<string, Type>();

        static SimpleSerializer()
        {
            SimpleSerializer.RegisterType(typeof(ObjectModel).Name, typeof(ObjectModel));
            SimpleSerializer.RegisterType(typeof(ObjectInstance).Name, typeof(ObjectInstance));
        }

        public static void RegisterType(string typeName, Type type)
        {
            if(type.GetInterface(typeof(IBinarySerializable).Name) == null)
            {
                throw new Exception("Попытка зарегистрировать несериализируемый тип.");
            }
            if (SimpleSerializer.registereredTypes.ContainsKey(typeName))
            {
                throw new Exception("Попытка повторно зарегистрировать тип.");
            }
            SimpleSerializer.registereredTypes.Add(typeName, type);
        }

        #region Serialization

        public static void PackObject(BinaryWriter writer, IBinarySerializable serializableObj)
        {
            if(serializableObj == null)
            {
                throw new Exception("Попытка упаковать пустоту");
            }
            else
            {
                serializableObj.Pack(writer);
            }
        }

        public static IBinarySerializable UnpackObject(BinaryReader reader)
        {
            string typeName = reader.ReadString();

            if(typeName.Length == 0)
            {
                return null;
            }

            Type findedType = null;
            if (SimpleSerializer.registereredTypes.ContainsKey(typeName))
            {
                findedType = SimpleSerializer.registereredTypes[typeName];
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
