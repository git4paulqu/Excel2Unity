using Excel2Unity.Source.Meta.Type;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueDecoratorString : ValueDecorator
    {
        public ValueDecoratorString(string value, TypeDecorator decorator) : base(value, decorator)
        {
            this.value = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        public override object GetValue()
        {
            return value;
        }
    }
}
