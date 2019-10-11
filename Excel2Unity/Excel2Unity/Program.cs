using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Excel2Unity
{
    class Program
    {
        static void Main(string[] args)
        {
            bool success = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                if (!Source.Define.UserDefine.Initialize(args))
                    return;

                Source.Generator.Gen();
                success = true;
            }
            catch (Exception e)
            {
                success = false;
                Source.Common.Utility.Logger.Log(e.Message.ToString());
            }
            finally
            {
                //Gener.Finish(success);
                string result = success ? "成功" : "失败";
                Source.Common.Utility.Logger.Log("{0}, cost time:{1}ms, press any key exit.", result, sw.ElapsedMilliseconds.ToString());
                Console.ReadLine();

                System.Environment.Exit(0);
            }

            Console.ReadKey();
        }

        private static void WriteDefine()
        {
            Source.Define.UserDefine define = new Source.Define.UserDefine();

            define.excel = new Source.Define.ExcelDefine();

            define.path = new Source.Define.PathDefine();
            define.path.excelSourcePath = new List<string>();
            define.path.excelSourcePath.Add("1");
            define.path.excelSourcePath.Add("2");
            define.nameSpace = "@@";

            string filepath = Environment.CurrentDirectory + "/test";

            string json = LitJson.JsonMapper.ToJson(define);
            Source.Common.Utility.File.DeleteFile(filepath);
            Source.Common.Utility.File.WriteString2File(filepath, json);
        }

        private static void ReadDefine()
        {
            //string json = Source.Common.Utility.File.ReadStringByFile(filePath);
            //Source.Define.UserDefine define = LitJson.JsonMapper.ToObject<Source.Define.UserDefine>(json);
        }
    }
}
