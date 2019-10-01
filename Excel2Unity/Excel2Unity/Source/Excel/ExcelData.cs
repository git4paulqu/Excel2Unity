using System.Data;
using System.Text.RegularExpressions;

namespace Excel2Unity.Source.Excel
{
    public class ExcelData
    {
        public ExcelData(System.Data.DataTable data)
        {
            name = Regex.Match(data.TableName, "^[a-z]*", RegexOptions.IgnoreCase).Value;
            Common.Utility.Asserter.AssertStringNotNull(name, string.Format("excel sheet name [0] is eroor.", data.TableName));

            this.data = data;
            Common.Utility.Logger.Log("read excel： " + name);
        }

        public string name { get; private set; }
        public System.Data.DataTable data { get; private set; }
    }
}
