using Excel;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Excel2Unity.Source.Excel
{
    public class FileReader
    {
        public static void Read()
        {
            Common.Utility.Logger.Log("read excel files >>>");
            datas = new List<ExcelData>();

            foreach (string path in Define.UserDefine.global.path.excelSourcePath)
            {
                ReadExcelFile(path);
            }

            Common.Utility.Asserter.Assert(datas.Count > 0, "please make sure your excel path is right, or the .xlsx file num is greater than 0.");
        }

        private static void ReadExcelFile(string file)
        {
            Common.Utility.File.RecursionFileExecute(file, EXCEL_FILE_SUFFIX, (filePath) =>
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs))
                    {
                        DataSet result = excelReader.AsDataSet();

                        foreach (System.Data.DataTable item in result.Tables)
                        {
                            ExcelData excelData = new ExcelData(item);
                            datas.Add(excelData);
                        }
                    }
                }
            });
        }

        public static List<ExcelData> datas { get; private set; }

        private const string EXCEL_FILE_SUFFIX = "xlsx";
    }
}
