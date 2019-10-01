using System;

namespace Excel2Unity.Source.Define
{
    public class UserDefine : IDefine
    {
        public static bool Initialize()
        {
            try
            {
                string json = Source.Common.Utility.File.ReadStringByFile(PathDefine.configJsonFile);
                Source.Common.Utility.Asserter.AssertStringNotNull(json, "[user define] the config can not be null.");

                global = LitJson.JsonMapper.ToObject<Source.Define.UserDefine>(json);
                global.path.Initialize();

                global.readerTemplete = Common.Utility.File.ReadStringByFile(Define.PathDefine.readerTempletePath);
                Source.Common.Utility.Asserter.AssertStringNotNull(json, "[user define] the readerTemplete can not be null.");

                global.managerTemplete = Common.Utility.File.ReadStringByFile(Define.PathDefine.managerTempletePath);
                Source.Common.Utility.Asserter.AssertStringNotNull(json, "[user define] the managerTemplete can not be null.");

                return global.IsValid();
            }
            catch (Exception e)
            {
                Common.Utility.Logger.Log("[user define] initialize error, please make your config is right.");
                throw e;
            }
        }

        public bool IsValid()
        {
            if (null == excel || 
                null == path ||
                !excel.IsValid() ||
                !path.IsValid())
            {
                return false;
            }

            return true;
        }

        public string nameSpace { get; set; }
        public string readerTemplete { get; private set; }
        public string managerTemplete { get; private set; }
        public ExcelDefine excel { get; set; }
        public PathDefine path { get; set; }
        public static UserDefine global { get; private set; }
    }
}