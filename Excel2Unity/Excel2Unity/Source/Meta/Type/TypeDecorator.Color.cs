namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorColor : TypeDecorator
    {
        public TypeDecoratorColor(string original) : base(original)
        {
            typeDecotrator = EDecotratorType.Color;
        }

        public override string GetName()
        {
            return Define.ConstDefine.COLOR_DEFINE_TOSTRING;
        }
    }
}
