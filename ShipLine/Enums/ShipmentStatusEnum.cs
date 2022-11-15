namespace ShipLine.Enums
{
    public enum ShipmentStatusEnum
    {       
        InProgress,
        Shipping,
        Shipped,
        Returned
    }
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
