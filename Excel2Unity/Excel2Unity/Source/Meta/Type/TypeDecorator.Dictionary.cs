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
            Check();
        }

        public override string GetName()
        {
            return string.Format(Define.ConstDefine.DICTIONARY_DEFINE_TOSTRING, key.GetName(), value.GetName());
        }

        private void Check()
        {
            if (key.typeDecotrator != EDecotratorType.Int &&
                key.typeDecotrator != EDecotratorType.String &&
                key.typeDecotrator != EDecotratorType.Float)
            {
                throw new System.Exception(string.Format("Dictionary type is only support int/float/string, this key is {0}.", key.GetName()));
            }

            if (value.typeDecotrator == EDecotratorType.Dictionary)
            {
                throw new System.Exception("Dictionary value type is not support Dictionary.");
            }

            if (value.typeDecotrator == EDecotratorType.List)
            {
                TypeDecoratorList typeDecoratorList = value as TypeDecoratorList;
                if (typeDecoratorList.child.typeDecotrator != EDecotratorType.Int ||
                    typeDecoratorList.child.typeDecotrator != EDecotratorType.Float ||
                    typeDecoratorList.child.typeDecotrator != EDecotratorType.Bool ||
                    typeDecoratorList.child.typeDecotrator != EDecotratorType.String)
                {
                    throw new System.Exception("Dictionary value type is list, but the list child type is only support int/float/bool/string.");
                }
            }
        }

        public TypeDecorator key { private set; get; }

        public TypeDecorator value { private set; get; }
    }
}
