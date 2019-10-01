using System;
using System.Collections.Generic;

namespace Excel2Unity.Source.Define
{
    public class PathDefine : IDefine
    {
        public void Initialize()
        {
            tempRootPath = FormatWorkDir("temp");
            tempProtoPath = FormatWorkDir("temp/proto");
            tempCSPath = FormatWorkDir("temp/cs");
            tempNotComplierPath = FormatWorkDir("temp/notcompliercs");
            tempBinPath = FormatWorkDir("temp/bin");
        }

        public bool IsValid()
        {
            Common.Utility.Asserter.Assert(null != excelSourcePath && excelSourcePath.Count > 0, "[user define] excelSourcePath can not be null.");
            Common.Utility.Asserter.AssertStringNotNull(outputCSPath, "[user define] outputCSPath can not be null.");
            Common.Utility.Asserter.AssertStringNotNull(outputBinPath, "[user define] outputBinPath can not be null.");
            
            return true;
        }

        private string FormatWorkDir(string relativelyPath)
        {
            return string.Format("{0}/{1}", workPath, relativelyPath);
        }

        public string tempRootPath { get; private set; }
        public string tempProtoPath { get; private set; }
        public string tempCSPath { get; private set; }
        public string tempBinPath { get; private set; }
        public string tempNotComplierPath { get; private set; }

        public List<string> excelSourcePath { get; set; }
        public string outputCSPath { get; set; }
        public string outputBinPath { get; set; }

        #region static path
        public static string configJsonFile {
            get {
                return string.Format("{0}/config", workPath);
            }
        }

        public static string csLibsPath
        {
            get
            {
                return string.Format("{0}/libs", workPath);
            }
        }

        public static string readerTempletePath
        {
            get
            {
                return string.Format("{0}/templete/reader.templete", workPath);
            }
        }

        public static string managerTempletePath
        {
            get
            {
                return string.Format("{0}/templete/manager.templete", workPath);
            }
        }

        public static string workPath = Environment.CurrentDirectory;
        #endregion
    }
}
