using System;
using System.Collections.Generic;

namespace Excel2Unity.Source.Define
{
    public class UserDefine : IDefine
    {
        public static bool Initialize(string[] args)
        {
            try
            {
                string json = Source.Common.Utility.File.ReadStringByFile(PathDefine.configJsonFile);
                Source.Common.Utility.Asserter.AssertStringNotNull(json, "[user define] the config can not be null.");

                global = LitJson.JsonMapper.ToObject<Source.Define.UserDefine>(json);
                global.path.Initialize();
                ReorientatePath(args);

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

        private static void ReorientatePath(string[] args)
        {
            List<string> inputs = new List<string>();

            foreach (string item in args)
            {
                if (item.StartsWith("-i:"))
                {
                    inputs.Add(item.Substring(3));
                }

                if (item.StartsWith("-ocs:"))
                {
                    global.path.outputCSPath = item.Substring(5);
                }

                if (item.StartsWith("-obin:"))
                {
                    global.path.outputBinPath = item.Substring(6);
                }

                if (item.StartsWith("-n:"))
                {
                    global.nameSpace = item.Substring(3);
                }
            }
            global.path.excelSourcePath = inputs;
        }

        public string nameSpace { get; set; }
        public string readerTemplete { get; private set; }
        public string managerTemplete { get; private set; }
        public ExcelDefine excel { get; set; }
        public PathDefine path { get; set; }
        public static UserDefine global { get; private set; }
    }
}