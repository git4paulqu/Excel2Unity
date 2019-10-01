using System.Collections.Generic;

namespace Excel2Unity.Source.CSharp
{
    public class CSObjectPropertyGroup
    {
        public CSObjectPropertyGroup(List<string> summary, List<string> property)
        {
            this.datas = new List<Data>();

            int count = summary.Count;
            for (int i = 0; i < count; i++)
            {
                Data data = new Data(summary[i], property[i]);
                this.datas.Add(data);
            }
        }

        public string GetSummary(int index)
        {
            if (null == datas || index < 0 || index >= datas.Count)
            {
                return string.Empty;
            }
            return null;
        }

        public List<Data> datas { get; private set; }

        public class Data
        {
            public Data(string summary, string name)
            {
                this.summary = summary;
                this.name = name;
            }

            public string summary { get; private set; }
            public string name { get; private set; }
        }
    }
}
