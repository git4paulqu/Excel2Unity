namespace Excel2Unity.Source.Define
{
    public class ConstDefine
    {
        #region Difine
        public const string INT_DEFINE_STRING_FLAG = "int";
        public const string FLOAT_DEFINE_STRING_FLAG = "^float\\d{0,}$";
        public const string BOOL_DEFINE_STRING_FLAG = "bool";
        public const string STRING_DEFINE_STRING_FLAG = "string";
        public const string VECTOR2_DEFINE_STRING_FLAG = "vector2";
        public const string VECTOR3_DEFINE_STRING_FLAG = "vector3";
        public const string COLOR_DEFINE_STRING_FLAG = "color";
        public const string REPEAT_DEFINE_STRING_FLAG = "(?<=\\[).*(?=\\])";
        public const char REPEAT_DEFINE_SPLIT_CHAR = ',';
        public const string EXTENDS_NAMESPACE = "DataTable";
        public const string BIN_FILE_SUFFIX = "bytes";
        #endregion

        #region tostring
        public static string INT_DEFINE_TOSTRING = "int";
        public static string FLOAT_DEFINE_TOSTRING = "float";
        public static string BOOL_DEFINE_TOSTRING = "bool";
        public static string STRING_DEFINE_TOSTRING = "string";
        public static string VECTOR2_DEFINE_TOSTRING = "DataTable.Vector2";
        public static string VECTOR3_DEFINE_TOSTRING = "DataTable.Vector3";
        public static string COLOR_DEFINE_TOSTRING = "DataTable.Color";
        public static string LIST_DEFINE_TOSTRING = "List<{0}>";
        public static string DICTIONARY_DEFINE_TOSTRING = "Dictionary<{0}, {1}>";
        #endregion

    }
}
