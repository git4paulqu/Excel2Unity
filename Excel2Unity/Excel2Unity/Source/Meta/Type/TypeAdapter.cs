using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Excel2Unity.Source.Meta.Type
{
    public class TypeAdapter
    {
        public static TypeDecorator Adapter(string type)
        {
            TypeDecorator decorator = InternalAdapter(type);
            if (null == decorator)
                throw new Exception(string.Format("type:{0} is not support", type));
            return decorator;
        }

        private static TypeDecorator InternalAdapter(string type)
        {
            try
            {
                type = type.Trim();
                TypeDecorator decorator;
                TryContainer(type, out decorator);
                if (null != decorator)
                    return decorator;

                return TryNormal(type);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("parse type is error, type is {0}, error is {2}", type, e.Message));
            }
        }

        private static void TryContainer(string type, out TypeDecorator decorator)
        {
            decorator = null;
            string itemType = null;
            if (IsDictionary(type, out itemType))
            {
                string[] pair = itemType.Trim().Split(Define.ConstDefine.REPEAT_DEFINE_SPLIT_CHAR);
                TypeDecorator key = InternalAdapter(pair[0]);
                TypeDecorator value = InternalAdapter(pair[1]);
                decorator = new TypeDecoratorDictionary(key, value);
                return;
            }

            if (IsList(type, out itemType))
            {
                TypeDecorator item = InternalAdapter(itemType);
                decorator = new TypeDecoratorList(item);
            }
        }

        private static TypeDecorator TryNormal(string type)
        {
            switch (type)
            {
                case Define.ConstDefine.INT_DEFINE_STRING_FLAG:
                    return new TypeDecoratorInt(Define.ConstDefine.INT_DEFINE_TOSTRING);

                case Define.ConstDefine.BOOL_DEFINE_STRING_FLAG:
                    return new TypeDecoratorBool(Define.ConstDefine.BOOL_DEFINE_TOSTRING);

                case Define.ConstDefine.STRING_DEFINE_STRING_FLAG:
                    return new TypeDecoratorString(Define.ConstDefine.STRING_DEFINE_TOSTRING);

                case Define.ConstDefine.VECTOR2_DEFINE_STRING_FLAG:
                    return new TypeDecoratorVector2(Define.ConstDefine.VECTOR2_DEFINE_TOSTRING);

                case Define.ConstDefine.VECTOR3_DEFINE_STRING_FLAG:
                    return new TypeDecoratorVector3(Define.ConstDefine.VECTOR3_DEFINE_TOSTRING);

                case Define.ConstDefine.COLOR_DEFINE_STRING_FLAG:
                    return new TypeDecoratorColor(Define.ConstDefine.COLOR_DEFINE_TOSTRING);
            }

            int flag = 0;
            if (IsFloat(type, out flag))
                return new TypeDecoratorFloat(Define.ConstDefine.FLOAT_DEFINE_TOSTRING, flag);

            return null;
        }

        public static bool IsList(string type, out string itemType)
        {
            itemType = null;
            Match match = Regex.Match(type, Define.ConstDefine.REPEAT_DEFINE_STRING_FLAG);
            if (match.Success)
            {
                itemType = match.Value;
                return true;
            }
            return false;
        }

        public static bool IsDictionary(string type, out string itemType)
        {
            itemType = null;
            Match match = Regex.Match(type, Define.ConstDefine.REPEAT_DEFINE_STRING_FLAG);
            if (match.Success)
            {
                string pair = match.Value;
                if (pair.Contains(Define.ConstDefine.REPEAT_DEFINE_SPLIT_CHAR))
                {
                    itemType = pair;
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool IsContainer(EDecotratorType type)
        {
            return type == EDecotratorType.List || type == EDecotratorType.Dictionary;
        }

        public static bool HadExtends(TypeDecorator td)
        {
            if (IsContainer(td.typeDecotrator))
            {
                TypeDecoratorList tdl = td as TypeDecoratorList;
                if (null != tdl)
                    return HadExtends(tdl.child);

                TypeDecoratorDictionary tdm = td as TypeDecoratorDictionary;
                if (null != tdl)
                    return HadExtends(tdm.value);
            }

            EDecotratorType type = td.typeDecotrator;
            return EXTEND_DECORTATORTYPE.Contains(type);
        }

        private static bool IsFloat(string type, out int flag)
        {
            flag = 0;
            Match mt1 = Regex.Match(type, Define.ConstDefine.FLOAT_DEFINE_STRING_FLAG);
            if (!mt1.Success)
                return false;

            Match mt2 = Regex.Match(mt1.Value, "\\d");
            int pow = mt2.Success ? int.Parse(mt2.Value) : 0;
            flag = (int)Math.Pow(10, pow);
            return true;
        }

        private static EDecotratorType[] EXTEND_DECORTATORTYPE = new[] {
            EDecotratorType.Vector2,
            EDecotratorType.Vector3,
            EDecotratorType.Color,
        };
        
    }
}
