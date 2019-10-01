using Excel2Unity.Source.Meta.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Unity.Source.CSharp
{
    public class CSCodeGenarator
    {
        public static void GenCode(List<CSObject> datas)
        {
            Common.Utility.Logger.Log("\r\n");
            Common.Utility.Logger.Log("gen cs >>>");

            sb_manger.Clear();
            hadnsName = !string.IsNullOrEmpty(Define.UserDefine.global.nameSpace);
            tab = hadnsName ? "\t" : string.Empty;

            int count = datas.Count;
            int max = count - 1;
            for (int i = 0; i < count; i++)
            {
                CSObject item = datas[i];
                try
                {
                    GenCSCode(item);
                    GenReaderCSCode(item, i == max);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            GenManagerCSCoode();
        }

        private static void GenCSCode(CSObject data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(codeHeader);
            sb.Append("\r\n");
            sb.Append(string.Format("using {0};\r\n", CSHelper.GetUseNameSpace()));

            string nameSpace = Define.UserDefine.global.nameSpace;
            if (data.HadCollections)
                sb.Append("using System.Collections.Generic;\r\n");
            if (data.HadExtends && nameSpace != Define.ConstDefine.EXTENDS_NAMESPACE)
                sb.Append(Define.ConstDefine.EXTENDS_NAMESPACE + ";\r\n");

            sb.Append("\r\n");
            if (hadnsName)
            {
                sb.Append("namespace ");
                sb.Append(nameSpace);
                sb.Append("\r\n{");
                sb.Append("\r\n");
            }

            sb.Append(tab);
            sb.Append(CSHelper.ClassAttribute());
            sb.Append("\r\n");

            if (data.HadExtends)
            {
                int index = 1;
                if (data.HadVector2)
                {
                    sb.Append(tab);
                    sb.Append(string.Format("[ProtoInclude({0}, typeof(DataTable.Vector2))]\r\n", index));
                    index++;
                }

                if (data.HadVector3)
                {
                    sb.Append(tab);
                    sb.Append(string.Format("[ProtoInclude({0}, typeof(DataTable.Vector3))]\r\n", index));
                    index++;
                }

                if (data.HadColor)
                {
                    sb.Append(tab);
                    sb.Append(string.Format("[ProtoInclude({0}, typeof(DataTable.Color))]\r\n", index));
                    index++;
                }
            }

            sb.Append(tab);
            sb.Append("public partial class ");
            sb.Append(data.name);
            sb.Append("\r\n");
            sb.Append(tab);
            sb.Append("{");

            // construction
            sb.Append("\r\n");
            sb.Append(tab);
            sb.Append("\t");
            sb.Append("public ");
            sb.Append(data.name);
            sb.Append("() {");

            StringBuilder propertyCode = new StringBuilder();
            int count = data.typeCount;
            for (int i = 0; i < count; i++)
            {

                TypeDecorator type = data.type.datas[i];
                CSObjectPropertyGroup.Data propertry = data.property.datas[i];
                if (TypeAdapter.IsContainer(type.typeDecotrator) || TypeAdapter.HadExtends(type))
                {
                    sb.Append("\r\n");
                    sb.Append(tab);
                    sb.Append("\t\t");
                    sb.Append(propertry.name);
                    sb.Append(" = new ");
                    sb.Append(type.GetName());
                    sb.Append("();");
                }

                propertyCode.Append("\r\n");
                propertyCode.Append(tab);
                propertyCode.Append("\t");
                propertyCode.Append("/// <summary>\r\n");
                propertyCode.Append(tab);
                propertyCode.Append("\t/// ");
                propertyCode.Append(propertry.summary);
                propertyCode.Append("\r\n");
                propertyCode.Append(tab);
                propertyCode.Append("\t/// </summary>\r\n");

                propertyCode.Append(tab);
                propertyCode.Append("\t");
                int index = i + 1;
                propertyCode.Append(CSHelper.Attribute(index));

                propertyCode.Append("\r\n");
                propertyCode.Append(tab);
                propertyCode.Append("\t");
                propertyCode.Append("public ");
                propertyCode.Append(type.GetName());

                propertyCode.Append(" ");
                propertyCode.Append(propertry.name);
                propertyCode.Append(" { private set; get; }");
                propertyCode.Append("\r\n");
            }

            sb.Append("\r\n");
            sb.Append("\t");
            sb.Append(tab);
            sb.Append("}");
            sb.Append("\r\n");

            sb.Append(propertyCode.ToString());
            sb.Append(tab);
            sb.Append("}");

            if (hadnsName)
            {
                sb.Append("\r\n");
                sb.Append("}");
            }

            CSHelper.Write(data.name, sb.ToString());
        }

        private static void GenReaderCSCode(CSObject data, bool ismax)
        {
            string code = Define.UserDefine.global.readerTemplete.Replace("#tab#", tab);
            code = code.Replace("#classname#", data.name);
            if (hadnsName)
                code = CSHelper.WrapNameSpace(Define.UserDefine.global.nameSpace, code);
            code = codeHeader + code;
            string filePath = string.Format("{0}/{1}DataReader.cs", Define.UserDefine.global.path.tempNotComplierPath, data.name);
            Common.Utility.File.WriteString2File(filePath, code);

            sb_manger.Append("\t\t\t");
            sb_manger.Append(string.Format("RegisterReader<{0}>(new {1}DataReader());", data.name, data.name));

            if (!ismax)
                sb_manger.Append("\r\n");
        }

        private static void GenManagerCSCoode()
        {
            string code = Define.UserDefine.global.managerTemplete.Replace("#tab#", tab);
            code = code.Replace("#Register#", sb_manger.ToString());
            if (hadnsName)
                code = CSHelper.WrapNameSpace(Define.UserDefine.global.nameSpace, code);

            code = codeHeader + code;
            string filePath = string.Format("{0}/DataTableManager.cs", Define.UserDefine.global.path.tempNotComplierPath);
            Common.Utility.File.WriteString2File(filePath, code);
        }

        private static bool hadnsName;
        private static string tab;
        private static StringBuilder sb_manger = new StringBuilder();

        private const string codeHeader = @"//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//     If you have other needs, you can write another partial class.
// </auto-generated>
//------------------------------------------------------------------------------
";
    }
}
