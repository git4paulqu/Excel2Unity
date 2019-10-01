namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorBool : TypeDecorator
    {
        public TypeDecoratorBool(string original) : base(original)
        {
            typeDecotrator = EDecotratorType.Bool;
        }

        public override string GetName()
        {
            return Define.ConstDefine.BOOL_DEFINE_TOSTRING;
        }
    }
}
