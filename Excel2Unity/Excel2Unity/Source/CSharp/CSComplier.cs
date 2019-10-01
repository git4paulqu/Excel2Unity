using Excel2Unity.Source.Common;
using Excel2Unity.Source.Meta.Type;
using Excel2Unity.Source.Meta.Value;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Excel2Unity.Source.CSharp
{
    public class CSComplier
    {
        public virtual void Complier(List<CSObject> datas)
        {
            this.datas = datas;
        }

        protected void OnCompiler()
        {
            Common.Utility.Logger.Log("\r\n");
            Common.Utility.Logger.Log("complier cs >>>");

            CompilerResults cr = GetComplierResults();
            Assembly assembly = cr.CompiledAssembly;
            for (int i = 0; i < datas.Count; i++)
            {
                SerializeCSData(assembly, datas[i]);
            }
        }

        protected virtual void OnCompilerParameters(ref CompilerParameters param)
        {

        }

        protected virtual byte[] Serialize(object instance)
        {
            return null;
        }

        private CompilerResults GetComplierResults()
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameters = new CompilerParameters();
            parameters.CompilerOptions = "/target:library /optimize /warn:0";
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
            parameters.IncludeDebugInformation = true;
            parameters.ReferencedAssemblies.Add("System.dll");

            string codepath = Define.UserDefine.global.path.tempCSPath;
            OnCompilerParameters(ref parameters);
            string[] csharp_files = Common.Utility.File.GetAllFileNamesByPath(codepath, new string[] { "cs" }).ToArray<string>();
            CompilerResults results = provider.CompileAssemblyFromFile(parameters, csharp_files);

            if (results.NativeCompilerReturnValue != 0)
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine(string.Format("return code: {0}", results.NativeCompilerReturnValue));

                foreach (CompilerError error in results.Errors)
                {
                    if (error.IsWarning)
                    {
                        builder.Append("<WARNING> ");
                    }
                    else
                    {
                        builder.Append("<ERROR> ");
                    }


                    builder.AppendLine(string.Format("{0}({1},{2}): error {3}: {4}", error.FileName
                                                                                    , error.Line
                                                                                    , error.Column
                                                                                    , error.ErrorNumber
                                                                                    , error.ErrorText));

                }

                throw new System.Exception(builder.ToString());
            }

            return results;
        }

        private void SerializeCSData(Assembly assembly, CSObject data)
        {
            Common.Utility.Logger.Log(string.Format("complier {0}.cs", data.name));
            Type type = assembly.GetType(data.fullName, true);

            byte[] serialized = null;
            using (MemoryStream ms = new MemoryStream())
            {
                int count = data.dataCount;
                for (int i = 0; i < count; i++)
                {
                    CSObjectData obj = data.objectDatas[i];
                    SerializeCSObj(obj, type, ms, assembly, data, i);
                }
                serialized = ms.ToArray();
            }

            string filename = Define.UserDefine.global.path.tempBinPath + "/" + data.name + "." + Define.ConstDefine.BIN_FILE_SUFFIX;
            Common.Utility.File.WriteBytes2File(filename, serialized);
            Common.Utility.Logger.Log("write " + data.name + "." + Define.ConstDefine.BIN_FILE_SUFFIX);
        }

        private void SerializeCSObj(CSObjectData obj, Type type, MemoryStream stream, Assembly assembly, CSObject data, int row)
        {
            object instance = Activator.CreateInstance(type);
            PropertyInfo[] pis = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int key = 0;
            int count = pis.Length;
            for (int i = 0; i < count; i++)
            {
                PropertyInfo pi = pis[i];
                string value = obj.datas[i];
                TypeDecorator typeDecorator = data.type.datas[i];
                try
                {
                    typeDecorator.runtimeType = pi.PropertyType;
                    object objvalue = ValueAdapter.Adapter(value, typeDecorator);
                    pi.SetValue(instance, objvalue, null);

                    if (i == 0 && pi.PropertyType == typeof(int))
                        key = (int)objvalue;

                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("[{0}] complier data is error, the fild is:{1}, value is:{2}, row:{3}, column:{4},  {5}{6} ",
                        data.name,
                        data.property.datas[i].name,
                        value,
                        row + Define.UserDefine.global.excel.startIndex,
                        typeDecorator.index,
                        "\r\n", 
                        e.ToString()));
                }
            }

            byte[] data_byte = Serialize(instance);

            int length = sizeof(int);
            int data_length = data_byte.Length;
            int all_length = length + length + data_length;
            byte[] write = new byte[all_length];
            byte[] length_byte = BitConverter.GetBytes(data_length);
            byte[] key_byte = BitConverter.GetBytes(key);

            Buffer.BlockCopy(key_byte, 0, write, 0, length);
            Buffer.BlockCopy(length_byte, 0, write, length, length);
            Buffer.BlockCopy(data_byte, 0, write, length + length, data_length);

            stream.Write(write, 0, all_length);
        }

        protected List<CSObject> datas;
    }

    public class PBCSComplier : CSComplier
    {
        public static void ComplierObjects(List<CSObject> datas)
        {
            if (null == datas || datas.Count < 1)
            {
                return;
            }
            PBCSComplier complier = new PBCSComplier();
            complier.Complier(datas);
        }

        public override void Complier(List<CSObject> datas)
        {
            base.Complier(datas);
            OnCompiler();
        }

        protected override void OnCompilerParameters(ref CompilerParameters param)
        {
            param.ReferencedAssemblies.Add("protobuf-net.dll");
        }

        protected override byte[] Serialize(object instance)
        {
            byte[] result = null;
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, instance);
                result = ms.ToArray();
            }
            return result;
        }
    }
}
