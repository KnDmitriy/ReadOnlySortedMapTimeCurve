namespace TimeReadOnlySortedMap
{
    /// <summary>
    /// Определяет начало отсчета времени.
    /// </summary>
    public enum TimeOrigin
    {
        /// <summary>
        /// Наименьшее время в кривой.
        /// </summary>
        [Localization.AlternativeMemberName("Время начала исследования")]
        StartTime,

        /// <summary>
        /// Начало дня для наименьшего времени в кривой.
        /// </summary>
        [Localization.AlternativeMemberName("День начала исследования")]
        StartOfDay,
    }
}
