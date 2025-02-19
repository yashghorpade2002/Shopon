namespace ShoponConsoleApplication.Utils
{
    /// <summary>
    /// The CustomConverter
    /// </summary>
    public static class CustomConverter
    {
        /// <summary>
        /// The GetInt. This method is used to convert string to int.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int GetInt(string data)
        {
            int result = 0;
            int.TryParse(data, out result);
            return result;
        }

        /// <summary>
        /// Method to convert string to double.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double GetDouble(string data)
        {
            double result = 0;
            double.TryParse(data, out result);
            return result;
        }

    }
}
