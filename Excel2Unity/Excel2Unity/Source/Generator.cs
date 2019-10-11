namespace Excel2Unity.Source
{
    public class Generator
    {
        public static void Gen()
        {
            ClearTempDirectories();
            CopyLibCSharpFiles();

            Source.Excel.FileReader.Read();
            Source.CSharp.CSReader.ParseFromExcel(Source.Excel.FileReader.datas);
            Source.CSharp.CSCodeGenarator.GenCode(Source.CSharp.CSReader.datas);
            Source.CSharp.PBCSComplier.ComplierObjects(Source.CSharp.CSReader.datas);
            Source.FileCopyer.Start();
        }

        private static void ClearTempDirectories()
        {
            Source.Common.Utility.File.TryClearPath(Define.UserDefine.global.path.tempRootPath);
            Source.Common.Utility.File.CreatFolder(Define.UserDefine.global.path.tempCSPath);
            Source.Common.Utility.File.CreatFolder(Define.UserDefine.global.path.tempProtoPath);
            Source.Common.Utility.File.CreatFolder(Define.UserDefine.global.path.tempBinPath);
            Source.Common.Utility.File.CreatFolder(Define.UserDefine.global.path.tempNotComplierPath);
        }

        private static void CopyLibCSharpFiles()
        {
            if (Common.Utility.File.DirectoryExists(Define.PathDefine.csLibsPath))
            {
                Common.Utility.File.CopyDirectories(Define.PathDefine.csLibsPath, Define.UserDefine.global.path.tempCSPath);
            }
        }
    }
}
