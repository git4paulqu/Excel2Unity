namespace Excel2Unity.Source.Meta.Type
{
    public abstract class TypeDecorator
    {
        public TypeDecorator(string original)
        {
            originalData = original;
        }

        public abstract string GetName();

        public System.Type runtimeType { get; set; }
        public int flag { get; protected set; }
        public int index { get; set; }
        public string originalData { get; protected set; }
        public EDecotratorType typeDecotrator { get; protected set; }
    }
}
