namespace ViaCepClient.Validators
{
    /// <summary>
    /// IModelError represents an 
    /// interface for model errors
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Message represents the message error
        /// </summary>
        string Message { get; }
    }
}