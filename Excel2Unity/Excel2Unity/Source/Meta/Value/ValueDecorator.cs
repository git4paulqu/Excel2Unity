using Excel2Unity.Source.Meta.Type;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueDecorator
    {
        public ValueDecorator(string value, TypeDecorator decorator)
        {
            this.value = value.Trim();
            this.decorator = decorator;
        }

        public virtual object GetValue()
        {
            return null;
        }

        protected string value;
        protected TypeDecorator decorator;
    }
}
