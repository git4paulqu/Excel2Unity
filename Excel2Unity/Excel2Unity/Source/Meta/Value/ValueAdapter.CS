using Excel2Unity.Source.Meta.Type;
using System;

namespace Excel2Unity.Source.Meta.Value
{
    public class ValueAdapter
    {
        public static object Adapter(string value, TypeDecorator type)
        {
            try
            {
                switch (type.typeDecotrator)
                {
                    case EDecotratorType.Int:
                        return new ValueDecoratorInt(value, type).GetValue();

                    case EDecotratorType.Float:
                        return new ValueDecoratorFloat(value, type).GetValue();

                    case EDecotratorType.Bool:
                        return new ValueDecoratorBool(value, type).GetValue();

                    case EDecotratorType.String:
                        return new ValueDecoratorString(value, type).GetValue();

                    case EDecotratorType.Vector2:
                        return new ValueDecoratorVector2(value, type).GetValue();

                    case EDecotratorType.Vector3:
                        return new ValueDecoratorVector3(value, type).GetValue();

                    case EDecotratorType.Color:
                        return new ValueDecoratorColor(value, type).GetValue();

                    case EDecotratorType.List:
                        return new ValueDecoratorList(value, type).GetValue();

                    case EDecotratorType.Dictionary:
                        return new ValueDecoratorDictionary(value, type).GetValue();

                    default:
                        throw new Exception("parse value error, type is " + type.GetName());
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("parse value error, type is {0}, value is {1}, error is {2}", type.GetName(), value, e));
            }
        }
    }
}
