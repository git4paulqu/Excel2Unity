using Excel2Unity.Source.Meta.Type;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueDecoratorFloat : ValueDecorator
    {
        public ValueDecoratorFloat(string value, TypeDecorator decorator) : base(value, decorator)
        {
            this.value = string.IsNullOrEmpty(value) ? "0" : value;
        }

        public override object GetValue()
        {
            return float.Parse(value) / (float)decorator.flag;
        }
    }
}
