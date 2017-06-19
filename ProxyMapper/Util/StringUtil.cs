namespace ProxyMapper.Util
{
    internal class StringUtil
    {
        public static string CapitalizeFirstLetter(string input)
        {
            if (input == null)
            {
                return null;
            }
            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }

        public static string ConvertToPascalCase(string input)
        {
            if (input == null)
            {
                return null;
            }
            return CapitalizeFirstLetter(input.Replace("_", string.Empty));
        }
    }
}