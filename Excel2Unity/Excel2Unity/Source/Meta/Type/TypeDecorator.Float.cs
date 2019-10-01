namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorFloat : TypeDecorator
    {
        public TypeDecoratorFloat(string original, int flag) : base(original)
        {
            typeDecotrator = EDecotratorType.Float;
            this.flag = flag;
        }

        public override string GetName()
        {
            return Define.ConstDefine.FLOAT_DEFINE_TOSTRING;
        }
    }
}
