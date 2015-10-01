using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RESerializable
{
    public interface IStringSerializable
    {
        string StringSerialize();
        void StringDeserialize(string toDesetialize);
    }
}
