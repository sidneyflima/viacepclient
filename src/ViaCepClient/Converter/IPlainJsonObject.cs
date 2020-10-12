namespace ViaCepClient.Converter
{
    /// <summary>
    /// IPlainJsonObject represents a reader wrapper 
    /// for plain json reading
    /// </summary>
    public interface IPlainJsonObject
    {
        /// <summary>
        /// Get json value by key. If key not exists, return empty string
        /// </summary>
        string this[string key] { get; }
    }
}