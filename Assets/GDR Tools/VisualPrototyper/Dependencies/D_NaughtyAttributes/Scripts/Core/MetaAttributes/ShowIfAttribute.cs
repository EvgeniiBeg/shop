using System;

namespace D_NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ShowIfAttribute : ShowIfAttributeBase
    {
        /*public ShowIfAttribute(string condition)
            : base(condition)
        {
            Inverted = false;
        }*/

        public ShowIfAttribute(string condition, bool inverted = false)
            : base(condition)
        {
            Inverted = inverted;
        }

        public ShowIfAttribute(EConditionOperator conditionOperator, params string[] conditions)
            : base(conditionOperator, conditions)
        {
            Inverted = false;
        }

        public ShowIfAttribute(string enumName, object enumValue)
            : base(enumName, enumValue as Enum)
        {
            Inverted = false;
        }
    }
}
