namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorString : TypeDecorator
    {
        public TypeDecoratorString(string original) : base(original)
        {
            typeDecotrator = EDecotratorType.String;
        }

        public override string GetName()
        {
            return Define.ConstDefine.STRING_DEFINE_TOSTRING;
        }
    }
}
