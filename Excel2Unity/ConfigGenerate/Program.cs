using System;
using Excel2Unity.Source.Define;

namespace ConfigGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            UserDefine userDefine = new UserDefine();
            userDefine.path = new PathDefine();
            ExcelDefine excelDefine = new ExcelDefine();
            excelDefine.summaryIndex = 0;
            excelDefine.propertyIndex = 1;
            excelDefine.typeIndex = 2;
            excelDefine.splitFlag0 = ';';
            excelDefine.splitFlag1 = '|';
            excelDefine.ignoreFlag = "@";
            excelDefine.summaryIndex = 0;
            userDefine.excel = excelDefine;

            bool showHelp = false;
            foreach (string item in args)
            {
                if (item.StartsWith("-i:"))
                {
                    userDefine.path.excelSourcePath.Add(item.Substring(3));
                }

                if (item.StartsWith("-ocs:"))
                {
                    userDefine.path.outputCSPath = item.Substring(5);
                }

                if (item.StartsWith("-obin:"))
                {
                    userDefine.path.outputBinPath = item.Substring(6);
                }

                if (item.StartsWith("-n:"))
                {
                    userDefine.nameSpace = item.Substring(3);
                }

                if (item.StartsWith("-h:"))
                {
                    showHelp = true;
                }
            }

            if (showHelp)
            {
                ShowHelp();
                return;
            }

            string json = LitJson.JsonMapper.ToJson(userDefine);
            string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "config";
            Excel2Unity.Source.Common.Utility.File.DeleteFile(filepath);
            Excel2Unity.Source.Common.Utility.File.WriteString2File(filepath, json);
        }



        private static void ShowHelp()
        { // deliberately mimicking "protoc"'s calling syntax
            Console.WriteLine(@"Usage: [OPTION] generate config file:
  -i:                         Specify the directory in which to search for
                              imports.  May be specified multiple times;
                              directories will be searched in order. 
  -ocs:                       Specify the directory in which to output the code files.
  -obin:                      Specify the directory in which to output the byte files.
  -n:                         Specify the code namespace.
  -h                          Show this text and exit.");
        }
    }
}
