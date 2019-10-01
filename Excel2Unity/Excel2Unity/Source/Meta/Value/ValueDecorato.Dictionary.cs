using Excel2Unity.Source.Meta.Type;
using System;
using System.Reflection;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueDecoratorDictionary : ValueDecorator
    {
        public ValueDecoratorDictionary(string type, TypeDecorator decorator) : base(type, decorator)
        {

        }

        public override object GetValue()
        {
            try
            {
                System.Type runtimeType = decorator.runtimeType;
                System.Type[] itemTypes = runtimeType.GetGenericArguments();
                string[] keyItems;
                string[] valueItems;
                GetKeyValues(out keyItems, out valueItems);
                TypeDecoratorDictionary type = (decorator as TypeDecoratorDictionary);
                object[] keys = GetItemValues(type.key, itemTypes[0], keyItems);
                object[] values = GetItemValues(type.value, itemTypes[0], valueItems);
                if (keys.Length != values.Length)
                    throw new Exception("key length != value length.");

                object obj = Activator.CreateInstance(runtimeType);
                BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
                MethodInfo methodInfo = runtimeType.GetMethod("Add", flag);
                for (int i = 0; i < keys.Length; i++)
                    methodInfo.Invoke(obj, new object[] { keys[i], values[i] });
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception("try get map value is error, " + e.ToString());
            }
        }

        private object[] GetItemValues(TypeDecorator type, System.Type runtimeType, string[] values)
        {
            if (TypeAdapter.IsContainer(type.typeDecotrator))
                throw new Exception(string.Format("map key or value type is container, the type is {0}.", type.GetName()));

            if (null == runtimeType || null == values || values.Length < 1)
                return new object[0];

            int count = values.Length;
            object[] obj_values = new object[count];

            type.runtimeType = runtimeType;
            for (int i = 0; i < count; i++)
            {
                obj_values[i] = ValueAdapter.Adapter(values[i], type);
            }
            return obj_values;
        }

        private void GetKeyValues(out string[] keys, out string[] values)
        {
            keys = null;
            values = null;
            string[] paris = value.Split(Define.UserDefine.global.excel.splitFlag0);
            if (paris.Length <= 1)
                return;

            keys = new string[paris.Length];
            values = new string[paris.Length];
            for (int i = 0; i < paris.Length; i++)
            {
                string[] kvs = paris[i].Split(Define.UserDefine.global.excel.splitFlag1);
                keys[i] = kvs[0];
                values[i] = kvs[1];
            }
        }

        public int depth { set; get; }
    }
}
