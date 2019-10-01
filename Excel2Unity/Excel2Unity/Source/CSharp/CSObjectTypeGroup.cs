using Excel2Unity.Source.Meta.Type;
using System.Collections.Generic;
using System.Data;

namespace Excel2Unity.Source.CSharp
{
    public class CSObjectTypeGroup
    {
        public CSObjectTypeGroup(DataRow dataRow, int columnCount, string groupName)
        {
            this.groupName = groupName;
            InternalConstruction(dataRow, columnCount);
        }

        private void InternalConstruction(DataRow dataRow, int columnCount)
        {
            datas = new List<TypeDecorator>();
            ignoreIndexs = new List<int>();

            for (int i = 0; i < columnCount; i++)
            {
                string data = Common.Utility.ParseDataRowValue(dataRow, i);
                if (data == Define.UserDefine.global.excel.ignoreFlag)
                {
                    ignoreIndexs.Add(i);
                    continue;
                }

                AdapterType(data, i);
            }
        }

        private void AdapterType(string data, int column)
        {
            try
            {
                TypeDecorator typeDecorator = TypeAdapter.Adapter(data);
                typeDecorator.index = column;
                datas.Add(typeDecorator);
            }
            catch (System.Exception e)
            {
                Common.Utility.Logger.Log("[{0}] AdapterType error, type is:{1}, column:{2}.", groupName, data, column);
                throw e;
            }
        }

        public bool HadCollections
        {
            get
            {
                if (null == datas || datas.Count < 1)
                {
                    return false;
                }

                foreach (var item in datas)
                {
                    if (TypeAdapter.IsContainer(item.typeDecotrator))
                        return true;
                }

                return false;
            }
        }

        public bool HadExtends
        {
            get
            {
                if (null == datas || datas.Count < 1)
                {
                    return false;
                }

                foreach (var item in datas)
                {
                    if (TypeAdapter.HadExtends(item))
                        return true;
                }

                return false;
            }
        }

        public bool HadVector2
        {
            get
            {
                if (null == datas || datas.Count < 1)
                {
                    return false;
                }

                foreach (var item in datas)
                {
                    if (item.typeDecotrator == EDecotratorType.Vector2)
                        return true;
                }

                return false;
            }
        }

        public bool HadVector3
        {
            get
            {
                if (null == datas || datas.Count < 1)
                {
                    return false;
                }

                foreach (var item in datas)
                {
                    if (item.typeDecotrator == EDecotratorType.Vector3)
                        return true;
                }

                return false;
            }
        }

        public bool HadColor
        {
            get
            {
                if (null == datas || datas.Count < 1)
                {
                    return false;
                }

                foreach (var item in datas)
                {
                    if (item.typeDecotrator == EDecotratorType.Color)
                        return true;
                }

                return false;
            }
        }

        public string groupName { get; private set; }
        public List<TypeDecorator> datas { get; private set; }
        public List<int> ignoreIndexs { get; private set; }
    }
}
