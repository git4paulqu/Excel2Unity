using System.Collections.Generic;

namespace Excel2Unity.Source.CSharp
{
    public class CSObjectData
    {
        public CSObjectData(List<string> data, int index)
        {
            datas = data;
            this.index = index;
        }

        public string key
        {
            get
            {
                if (null == datas || datas.Count < 1)
                {
                    return string.Empty;
                }
                return datas[0];
            }
        }

        public int index { get; private set; }

        public List<string> datas { get; private set; }
    }
}
