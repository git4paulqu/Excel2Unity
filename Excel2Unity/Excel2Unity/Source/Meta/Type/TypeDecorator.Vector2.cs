namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorVector2 : TypeDecorator
    {
        public TypeDecoratorVector2(string original) : base(original)
        {
            typeDecotrator = EDecotratorType.Vector2;
        }

        public override string GetName()
        {
            return Define.ConstDefine.VECTOR2_DEFINE_TOSTRING;
        }
    }
}
