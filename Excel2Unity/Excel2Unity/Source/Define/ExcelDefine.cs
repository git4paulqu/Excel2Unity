using System;

namespace Excel2Unity.Source.Define
{
    public class ExcelDefine : IDefine
    {
        public bool IsValid()
        {
            bool c1 = summaryIndex != propertyIndex;
            bool c2 = propertyIndex != typeIndex;

            Common.Utility.Asserter.Assert(c1, "[user define] summaryIndex:{0} can not equals propertyIndex:{1}.", summaryIndex, propertyIndex);
            Common.Utility.Asserter.Assert(c2, "[user define] propertyIndex:{0} can not equals typeIndex:{1}.", propertyIndex, typeIndex);

            Common.Utility.Asserter.AssertStringNotNull(splitFlag0.ToString(), "[user define] splitFlag0 can not be null.");
            Common.Utility.Asserter.AssertStringNotNull(splitFlag1.ToString(), "[user define] splitFlag0 can not be null.");
            Common.Utility.Asserter.AssertStringNotNull(ignoreFlag, "[user define] splitFlag0 can not be null.");

            return true;
        }

        public int summaryIndex { get; set; }
        public int propertyIndex { get; set; }
        public int typeIndex { get; set; }
        public int startIndex
        {
            get {
                return typeIndex + 1;
            }
        }
        public char splitFlag0 { get; set; }
        public char splitFlag1 { get; set; }
        public string ignoreFlag { get; set; }
    }
}
