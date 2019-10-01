using Excel2Unity.Source.Meta.Type;
using System;
using System.Reflection;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueDecoratorList : ValueDecorator
    {
        public ValueDecoratorList(string type, TypeDecorator decorator) : base(type, decorator)
        {

        }

        public override object GetValue()
        {
            System.Type runtimeType = decorator.runtimeType;
            System.Type[] itemTypes = runtimeType.GetGenericArguments();
            object[] items = GetItemValues(itemTypes[0]);

            object obj = Activator.CreateInstance(runtimeType);
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
            MethodInfo methodInfo = runtimeType.GetMethod("Add", flag);
            for (int i = 0; i < items.Length; i++)
                methodInfo.Invoke(obj, new object[] { items[i] });
            return obj;

        }

        private object[] GetItemValues(System.Type childRuntimeType)
        {
            TypeDecoratorList type = (decorator as TypeDecoratorList);
            if (null == type || string.IsNullOrEmpty(value))
                return new object[0];

            if (type.flag < 0)
                throw new System.Exception(string.Format("value depth is overflow, max depth is 2"));

            char split = type.flag == 0 ? Define.UserDefine.global.excel.splitFlag0 : Define.UserDefine.global.excel.splitFlag1;
            string[] str_values = value.Split(split);
            int count = str_values.Length;
            object[] obj_values = new object[count];

            TypeDecorator child = type.child;
            child.runtimeType = childRuntimeType;
            for (int i = 0; i < count; i++)
            {
                obj_values[i] = ValueAdapter.Adapter(str_values[i], child);
            }
            return obj_values;
        }

        public int depth { set; get; }
    }
}
