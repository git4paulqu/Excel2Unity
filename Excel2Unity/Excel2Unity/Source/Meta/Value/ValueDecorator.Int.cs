using Excel2Unity.Source.Meta.Type;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueDecoratorInt : ValueDecorator
    {
        public ValueDecoratorInt(string value, TypeDecorator decorator) : base(value, decorator)
        {
            this.value = string.IsNullOrEmpty(value) ? "0" : value;
        }

        public override object GetValue()
        {
            return int.Parse(value);
        }
    }
}
