using System;

namespace IBUtility
{
    public static class DoubleDecimalConverter
    {
        public static decimal DoubleToDecimal(double input)
        {
            decimal ret = 0;

            try
            {
                ret = System.Convert.ToDecimal(input);
            }
            catch (OverflowException ex)
            {
                ret = Math.Sign(input) > 0 ? Decimal.MaxValue : Decimal.MinValue;
            }
            return ret;
        }
    }
}
