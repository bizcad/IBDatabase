using System;

namespace IBDatabase
{
    public static class DoubleDecimalConverter
    {
        public static decimal DoubleToDecimal(double input)
        {
            decimal ret = 0;

            try
            {
                if (Math.Abs(input - double.MaxValue) < .00000000000000005)
                {
                    return Decimal.MaxValue;
                }
                if (Math.Abs(input - double.MinValue) < .00000000000000005)
                {
                    return Decimal.MinValue;
                }
                ret = System.Convert.ToDecimal(input);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Overflow Exception " + ex.Message);
                ret = Math.Sign(input) > 0 ? Decimal.MaxValue : Decimal.MinValue;
            }
            return ret;
        }
    }
}
