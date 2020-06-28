namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// Represents a rule result
    /// </summary>
    public interface IRuleResult
    {
        /// <summary>
        /// IsValid returns true if value is valid for rule specification
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Error returns an error message if value is invalid
        /// </summary>
        IError Error { get; }
    }
}