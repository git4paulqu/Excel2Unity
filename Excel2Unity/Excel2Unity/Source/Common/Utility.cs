using System;
using System.Data;

namespace Excel2Unity.Source.Common
{
    public partial class Utility
    {
        public static string ParseDataRowValue(DataRow dataRow, int index)
        {
            if (null == dataRow || null == dataRow.ItemArray || index < 0 || index >= dataRow.ItemArray.Length)
            {
                return string.Empty;
            }

            object original = dataRow[index];
            string parse = string.Empty;
            if (DBNull.Value != original)
            {
                parse = original.ToString();
            }
            return parse;
        }
    }
}