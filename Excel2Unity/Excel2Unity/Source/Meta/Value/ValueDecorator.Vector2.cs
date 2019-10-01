using Excel2Unity.Source.Meta.Type;
using System.Collections.Generic;
using System.Reflection;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueDecoratorVector2 : ValueDecorator
    {
        public ValueDecoratorVector2(string value, TypeDecorator decorator) : base(value, decorator)
        {
        }

        public override object GetValue()
        {
            TypeDecoratorList itemsDecorator = TypeAdapter.Adapter("[float]") as TypeDecoratorList;
            itemsDecorator.runtimeType = typeof(List<float>);
            itemsDecorator.ResetFlag(1);
            object vs = ValueAdapter.Adapter(value, itemsDecorator);
            List<float> realvs = vs as List<float>;

            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            object v2 = Common.Utility.Reflection.CreateInstance(decorator.runtimeType);
            Common.Utility.Reflection.SetValue(decorator.runtimeType, "x", v2, realvs.Count > 0 ? realvs[0] : 0, flags);
            Common.Utility.Reflection.SetValue(decorator.runtimeType, "y", v2, realvs.Count > 1 ? realvs[1] : 0, flags);
            return v2;
        }
    }
}
