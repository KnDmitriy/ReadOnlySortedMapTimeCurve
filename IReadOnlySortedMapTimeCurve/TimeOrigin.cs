namespace TimeReadOnlySortedMap
{
    /// <summary>
    /// Определяет начало отсчета времени
    /// </summary>
    public enum TimeOrigin
    {
        /// <summary>
        /// Время начала исследования (Наименьшее время в кривой)
        /// </summary>
        [Localization.AlternativeMemberName("Время начала исследования")]
        StartTime,

        /// <summary>
        /// Начало дня исследования (Начало дня для наименьшего времени в кривой)
        /// </summary>
        [Localization.AlternativeMemberName("Начало дня исследования")]
        StartOfDay,
    }
}
