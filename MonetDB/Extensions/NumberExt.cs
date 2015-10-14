namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class NumberExt
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ThreeNonZeroDigits(this double value)
        {
            if (value >= 100)
            {
                // No digits after the decimal.
                return value.ToString("0,0");
            }
            else if (value >= 10)
            {
                // One digit after the decimal.
                return value.ToString("0.0");
            }
            else
            {
                // Two digits after the decimal.
                return value.ToString("0.00");
            }
        }

    }
}
