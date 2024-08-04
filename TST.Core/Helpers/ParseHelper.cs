namespace TST.Core.Helpers
{
    public static class ParseHelper
    {
        public static string ToUniqueFileName(this string value)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(value);
                string extension = Path.GetExtension(value);
                string res = $"{fileName}-{DateTime.Now.ToString("yyMMddHHmmss")}{extension}";
                return res;
            }
            catch
            {
            }
            return value;
        }
    }
}
