using System;
using System.Collections.Generic;
using System.Data;

namespace Excel2Unity.Source.CSharp
{
    public class CSObject
    {
        public CSObject(Excel.ExcelData excelData)
        {
            try
            {
                name = excelData.name;
                InternalConstruction(excelData);
            }
            catch (Exception e)
            {
                Common.Utility.Logger.Log("parse excel:{0} to cs objects.", name);
                throw e;
            }
        }

        private void InternalConstruction(Excel.ExcelData excelData)
        {
            System.Data.DataTable data = excelData.data;
            CheckSourceData(data);

            DataRowCollection rows = data.Rows;

            int dataStartIndex = Define.UserDefine.global.excel.startIndex;
            if (rows.Count <= dataStartIndex)
            {
                throw new Exception(string.Format("Excel Data rows count:{0} can not less than excel start index:{1}", rows.Count, Define.UserDefine.global.excel.startIndex));
            }

            int columnCount = data.Columns.Count;
            type = new CSObjectTypeGroup(rows[Define.UserDefine.global.excel.typeIndex], columnCount, name);

            List<int> ignoreIndexs = type.ignoreIndexs;

            int propertyIndex = Define.UserDefine.global.excel.propertyIndex;
            List<string> propertyOriginal = FiterRowData(rows[propertyIndex], columnCount, ignoreIndexs, string.Format("propertyIndex:{0}", propertyIndex));

            int summaryIndex = Define.UserDefine.global.excel.summaryIndex;
            List<string> summaryOriginal = FiterRowData(rows[summaryIndex], columnCount, ignoreIndexs, string.Format("summaryIndex:{0}", summaryIndex));
            property = new CSObjectPropertyGroup(summaryOriginal, propertyOriginal);

            objectDatas = new List<CSObjectData>();
            for (int i = dataStartIndex; i < rows.Count; i++)
            {
                List<string> originalData = FiterRowData(rows[i], columnCount, ignoreIndexs, string.Format("Data Row:{0}", i));
                CSObjectData objectData = new CSObjectData(originalData, i);
                CheckObjectKey(objectData, i);
                objectDatas.Add(objectData);
            }
        }

        private List<string> FiterRowData(DataRow dataRow, int columnCount, List<int> ignoreIndexs, string flag = "")
        {
            int column = 0;
            try
            {
                List<string> datas = new List<string>();
                for (int i = 0; i < columnCount; i++)
                {
                    column = i;
                    if (ignoreIndexs.Contains(i))
                    {
                        continue;
                    }

                    string data = Common.Utility.ParseDataRowValue(dataRow, i);
                    datas.Add(data);
                }

                return datas;
            }
            catch (Exception e)
            {
                Common.Utility.Logger.Log("[{0}] FiterRowData error, column:{1}, flag:{2}", name, column, flag);
                throw e;
            }
        }

        private void CheckSourceData(System.Data.DataTable data)
        {
            int minRowCount = Define.UserDefine.global.excel.startIndex + 1;
            int minColumnCount = 1;

            int rowCount = null == data.Rows ? 0 : data.Rows.Count;
            if (rowCount < minRowCount)
            {
                
                throw new Exception(string.Format("[{0}] data is error, the data row count is {1}, min row count must be {2}.", name, rowCount, minRowCount));
            }

            int columnCount = null == data.Columns ? 0 : data.Columns.Count;
            if (columnCount < minColumnCount)
            {

                throw new Exception(string.Format("[{0}] data is error, the data column count is {1}, min column count must be {2}.", name, columnCount, minColumnCount));
            }
        }

        private void CheckObjectKey(CSObjectData objectData, int index)
        {
            string key = objectData.key;
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception(string.Format("[{0}] data had null key, row index is {1}.", name, index));
            }

            foreach (var item in objectDatas)
            {
                if (item.key == key)
                {
                    throw new Exception(string.Format("[{0}] data had repeated key : {1}, row index is {2} and {3}.", name, key, item.index, index));
                }
            }
        }

        public bool HadCollections
        {
            get
            {
                if (null == type)
                {
                    return false;
                }
                return type.HadCollections;
            }
        }

        public bool HadExtends
        {
            get
            {
                if (null == type)
                {
                    return false;
                }
                return type.HadExtends;
            }
        }

        public bool HadVector2
        {
            get
            {
                if (null == type)
                {
                    return false;
                }
                return type.HadVector2;
            }
        }

        public bool HadVector3
        {
            get
            {
                if (null == type)
                {
                    return false;
                }
                return type.HadVector3;
            }
        }

        public bool HadColor
        {
            get
            {
                if (null == type)
                {
                    return false;
                }
                return type.HadColor;
            }
        }

        public int typeCount
        {
            get
            {
                if (null == type)
                {
                    return 0;
                }
                return type.datas.Count;
            }
        }

        public int dataCount
        {
            get
            {
                if (null == objectDatas)
                {
                    return 0;
                }
                return objectDatas.Count;
            }
        }

        public string fullName
        {
            get
            {
                string nameSpace = Define.UserDefine.global.nameSpace;
                return string.IsNullOrEmpty(nameSpace) ? name : string.Format("{0}.{1}", nameSpace, name);
            }
        }

        public string name { get; private set; }

        public CSObjectTypeGroup type { get; private set; }
        public CSObjectPropertyGroup property { get; private set; }
        public List<CSObjectData> objectDatas { get; private set; }
    }
}
