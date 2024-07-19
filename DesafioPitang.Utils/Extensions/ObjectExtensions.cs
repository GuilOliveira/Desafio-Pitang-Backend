namespace DesafioPitang.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static string? String(this object value)
        {
            return value == null ? string.Empty : value.ToString();
        }
    }
}
