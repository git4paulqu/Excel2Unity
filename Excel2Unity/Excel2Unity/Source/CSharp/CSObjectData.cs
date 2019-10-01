using System.Collections.Generic;

namespace Excel2Unity.Source.CSharp
{
    public class CSObjectData
    {
        public CSObjectData(List<string> data)
        {
            datas = data;
        }

        public List<string> datas { get; private set; }
    }
}
