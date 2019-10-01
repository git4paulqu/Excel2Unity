using Excel2Unity.Source.Meta.Type;
using System.Collections.Generic;
using System.Reflection;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueDecoratorColor : ValueDecorator
    {
        public ValueDecoratorColor(string value, TypeDecorator decorator) : base(value, decorator)
        {
        }

        public override object GetValue()
        {
            TypeDecoratorList itemsDecorator = TypeAdapter.Adapter("[int]") as TypeDecoratorList;
            itemsDecorator.runtimeType = typeof(List<int>);
            itemsDecorator.ResetFlag(1);
            object vs = ValueAdapter.Adapter(value, itemsDecorator);
            List<int> realvs = vs as List<int>;
            int r = realvs.Count > 0 ? realvs[0] : 0;
            int g = realvs.Count > 1 ? realvs[1] : 0;
            int b = realvs.Count > 2 ? realvs[2] : 0;
            int a = realvs.Count > 3 ? realvs[3] : 255;

            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            object Color = Common.Utility.Reflection.CreateInstance(decorator.runtimeType);
            Common.Utility.Reflection.SetValue(decorator.runtimeType, "r", Color, (float)r / 255f, flags);
            Common.Utility.Reflection.SetValue(decorator.runtimeType, "g", Color, (float)g / 255f, flags);
            Common.Utility.Reflection.SetValue(decorator.runtimeType, "b", Color, (float)b / 255f, flags);
            Common.Utility.Reflection.SetValue(decorator.runtimeType, "a", Color, (float)a / 255f, flags);
            return Color;
        }
    }
}
