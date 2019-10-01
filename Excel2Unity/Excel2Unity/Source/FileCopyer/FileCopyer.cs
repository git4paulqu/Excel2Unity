using Excel2Unity.Source.Common;
using System.Collections.Generic;
using System.Linq;
using static Excel2Unity.Source.Common.Utility;

namespace Excel2Unity.Source
{
    public class FileCopyer
    {
        public static void Start()
        {
            Logger.Log("\r\n");
            Logger.Log("copy >>>");

            CopyBins();
            CopyCSharps();
        }

        private static void CopyBins()
        {
            List<FileData> diffs = new List<FileData>();
            FileColection.Compare(new string[] { Define.UserDefine.global.path.outputBinPath }, 
                                  new string[] { Define.UserDefine.global.path.tempBinPath },
                                  Define.ConstDefine.BIN_FILE_SUFFIX,
                                  ref diffs);
            Remove(diffs);
            Copy(Define.UserDefine.global.path.outputBinPath, diffs);
        }

        private static void CopyCSharps()
        {
            if (string.IsNullOrEmpty(Define.UserDefine.global.path.outputCSPath))
                return;

            List<FileData> diffs = new List<FileData>();
            FileColection.Compare(new string[] { Define.UserDefine.global.path.outputCSPath },
                new string[] { Define.UserDefine.global.path.tempCSPath,
                               Define.UserDefine.global.path.tempNotComplierPath }, 
                               "cs",
                               ref diffs);
            Remove(diffs);
            Copy(Define.UserDefine.global.path.outputCSPath, diffs);
        }

        private static void Remove(List<FileData> files)
        {
            foreach (FileData file in files)
            {
                if (file.flag <= (int)EFileDiff.src_diff)
                {
                    Remove(file.fullname);
                    Remove(string.Format("{0}.meta", file.fullname));
                }
            }
        }

        private static void Remove(string file)
        {
            if (Utility.File.FileExists(file))
            {
                Utility.File.DeleteFile(file);
                Logger.Log(string.Format("rm: {0}", Utility.File.GetFileNameByString(file)));
            }
        }

        private static void Copy(string path, List<FileData> files)
        {
            foreach (FileData file in files)
            {
                if (file.flag == (int)EFileDiff.same_diff)
                {
                    Copy(file.diffpath, file.fullname);
                }
                else if (file.flag == (int)(EFileDiff.dest_diff))
                {
                    string dest = file.fullname.Replace(file.path, Utility.File.FormatPath(path));
                    Copy(file.fullname, dest);
                }
            }
        }

        private static void Copy(string src, string dest)
        {

            if (Utility.File.FileExists(src))
            {
                Utility.File.CopyFile(src, dest);
                Logger.Log(string.Format("copy: {0}", Utility.File.GetFileNameByString(src)));
            }
        }
    }

    public class FileColection
    {
        public static void Compare(string[] srcPath, string[] destPath, string expandName, ref List<FileData> diff)
        {
            List<FileData> srcFiles = Collection(srcPath, expandName);
            List<FileData> destFiles = Collection(destPath, expandName);

            List<FileData> same_diif = srcFiles.Where(a => destFiles.Exists(t => a.IsDiff(t))).ToList();
            List<FileData> src_Diff = srcFiles.Where(a => !destFiles.Exists(t => a.name == t.name)).ToList();
            List<FileData> dest_Diff = destFiles.Where(a => !srcFiles.Exists(t => a.name == t.name)).ToList();

            ResetFlag(EFileDiff.same_diff, ref same_diif);
            ResetFlag(EFileDiff.src_diff, ref src_Diff);
            ResetFlag(EFileDiff.dest_diff, ref dest_Diff);

            FileData[] files = new FileData[same_diif.Count + src_Diff.Count + dest_Diff.Count];
            same_diif.CopyTo(files, 0);
            src_Diff.CopyTo(files, same_diif.Count);
            dest_Diff.CopyTo(files, same_diif.Count + src_Diff.Count);
            diff = files.ToList();
        }

        public static List<FileData> Collection(string[] paths, string expandName)
        {
            List<FileData> result = new List<FileData>();
            foreach (var filePath in paths)
            {
                Utility.File.RecursionFileExecute(filePath, expandName, (file) =>
                {
                    string name = Utility.File.GetFileNameByPath(file);
                    string path = Utility.File.FormatPath(Utility.File.GetFilePath(file));
                    string md5 = Utility.MD5.GetFileMD5(file);
                    result.Add(new FileData { name = name, path = path, fullname = Utility.File.FormatPath(file), md5 = md5 });
                });
            }
            return result;
        }

        private static void ResetFlag(EFileDiff flag, ref List<FileData> files)
        {
            foreach (FileData file in files)
            {
                file.flag = (int)flag;
            }
        }

    }

    public class FileData
    {
        public string path;
        public string name;
        public string fullname;
        public string diffpath;
        public string md5;
        public int flag;

        public bool IsDiff(FileData file)
        {
            if (file.name != name)
                return false;

            diffpath = file.fullname;
            return !file.md5.Equals(md5);
        }
    }

    public enum EFileDiff
    {
        same_diff = 0,
        src_diff = 1,
        dest_diff = 2,
    }
}
