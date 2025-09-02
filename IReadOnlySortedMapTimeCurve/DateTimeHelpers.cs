using System;

namespace IReadOnlySortedMapTimeCurve
{
    public static class DateTimeHelpers
    {
        public static DateTime CreateFromByteArray(byte[] bytes)
        {
            if (bytes is null)
                throw new ArgumentNullException(nameof(bytes));

            if (bytes.Length != 8)
                throw new ArgumentException("Byte array is wrong");

            var ticks = BitConverter.ToInt64(bytes, 0);
            
            return new DateTime(ticks);
        }

        public static byte[] CreateFromDateTime(DateTime dateTime)
        {
            var ticks = dateTime.Ticks;

            return BitConverter.GetBytes(ticks);
        }
    }
}
