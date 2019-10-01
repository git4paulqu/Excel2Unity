namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorVector3 : TypeDecorator
    {
        public TypeDecoratorVector3(string original) : base(original)
        {
            typeDecotrator = EDecotratorType.Vector3;
        }

        public override string GetName()
        {
            return Define.ConstDefine.VECTOR3_DEFINE_TOSTRING;
        }
    }
}
