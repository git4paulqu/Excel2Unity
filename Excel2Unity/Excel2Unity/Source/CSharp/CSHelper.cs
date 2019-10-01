using System.Text;

namespace Excel2Unity.Source.CSharp
{
    public class CSHelper
    {
        public static string Attribute(int index)
        {
            return string.Format("[ProtoMember({0})]", index.ToString());
        }

        public static string ClassAttribute()
        {
            return classAtribute;
        }

        public static string GetUseNameSpace()
        {
            return nameSpace;
        }

        public static void Write(string name, string code)
        {
            string codePath = string.Format("{0}/{1}.cs", Define.UserDefine.global.path.tempCSPath, name);
            Common.Utility.File.WriteString2File(codePath, code);
            Common.Utility.Logger.Log(string.Format("gen {0}.cs", name));
        }

        public static string WrapNameSpace(string nsname, string code)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("namespace ");
            sb.Append(nsname);
            sb.Append("\r\n{\r\n");
            sb.Append(code);
            sb.Append("\r\n}");
            return sb.ToString();
        }

        private const string classAtribute = "[ProtoContract]";
        private const string nameSpace = "ProtoBuf";
    }
}
