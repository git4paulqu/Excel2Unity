using System.Collections.Generic;

namespace Excel2Unity.Source.CSharp
{
    public class CSReader
    {
        public static void ParseFromExcel(List<Excel.ExcelData> excelDatas)
        {
            datas = new List<CSObject>();

            Common.Utility.Logger.Log("\r\n");
            Common.Utility.Logger.Log("parse excel to cs objects >>>");

            foreach (var item in excelDatas)
            {
                Common.Utility.Logger.Log("parse excel:{0}", item.name);
                CSObject cs = new CSObject(item);
                datas.Add(cs);
            }
        }


        public static List<CSObject> datas { get; private set; }
    }
}
