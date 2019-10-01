namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorDictionary : TypeDecorator
    {
        public TypeDecoratorDictionary(TypeDecorator key, TypeDecorator value) : base(null)
        {
            this.key = key;
            this.value = value;
            typeDecotrator = EDecotratorType.Dictionary;
            this.originalData = GetName();
        }

        public override string GetName()
        {
            return string.Format(Define.ConstDefine.DICTIONARY_DEFINE_TOSTRING, key.GetName(), value.GetName());
        }

        public TypeDecorator key { private set; get; }

        public TypeDecorator value { private set; get; }
    }
}
