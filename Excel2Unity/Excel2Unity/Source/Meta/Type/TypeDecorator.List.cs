namespace Excel2Unity.Source.Meta.Type
{
    public class TypeDecoratorList : TypeDecorator
    {
        public TypeDecoratorList(TypeDecorator item) : base(item.GetName())
        {
            this.child = item;
            typeDecotrator = EDecotratorType.List;
            this.flag = GetDepth();
        }

        public override string GetName()
        {
            return string.Format(Define.ConstDefine.LIST_DEFINE_TOSTRING, child.GetName());
        }

        public void ResetFlag(int flag)
        {
            this.flag = flag;
        }

        private int GetDepth()
        {
            if (null != child && child.typeDecotrator == EDecotratorType.List)
                (child as TypeDecoratorList).ResetFlag(child.flag + 1);
            return 0;
        }

        public TypeDecorator child { get; private set; }
    }
}
