using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace INVex.Common.Serialize.Base
{
    public interface IBinarySerializable
    {
        void Pack(BinaryWriter writer);
        void Unpack(BinaryReader reader);
    }
}
