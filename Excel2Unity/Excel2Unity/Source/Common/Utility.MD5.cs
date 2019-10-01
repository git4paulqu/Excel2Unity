using System.Security.Cryptography;
using System.Text;

namespace Excel2Unity.Source.Common
{
    public partial class Utility
    {
        public class MD5
        {
            public static string GetMD5(byte[] byteData)
            {
                byte[] data = md5.ComputeHash(byteData);
                return FormatMD5(data);
            }

            public static string GetFileMD5(string fileName)
            {
                byte[] byteData = Utility.File.ReadBytesByFile(fileName);
                return GetMD5(byteData);
            }

            private static string FormatMD5(byte[] md5Data)
            {
                StringBuilder sb = new StringBuilder();
                int count = md5Data.Length;
                for (int i = 0; i < count; i++)
                {
                    sb.Append(md5Data[i].ToString("x2"));
                }
                return sb.ToString();
            }

            private static System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
        }
    }
}
