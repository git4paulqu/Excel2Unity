namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorInt : TypeDecorator
    {
        public TypeDecoratorInt(string original) : base(original)
        {
            typeDecotrator = EDecotratorType.Int;
        }

        public override string GetName()
        {
            return Define.ConstDefine.INT_DEFINE_TOSTRING;
        }
    }
}
