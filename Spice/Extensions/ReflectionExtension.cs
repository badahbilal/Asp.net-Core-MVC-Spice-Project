namespace Spice.Extensions
{
    public static class ReflectionExtension
    {
        public static string getPropertyValue<T>(this T item, string proprtyName)
        {
            return item.GetType().GetProperty(proprtyName).GetValue(item, null).ToString();
        }
    }
}
