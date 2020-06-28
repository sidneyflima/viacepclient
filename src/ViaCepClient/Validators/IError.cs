namespace ViaCepClient.Validators
{
    /// <summary>
    /// IModelError represents an 
    /// interface for model errors
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Code represents a unique code representation for error
        /// </summary>
        string ErrorCode { get; }

        /// <summary>
        /// PropertyName represents the model property name which has an error
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Message represents the message error
        /// </summary>
        string Message { get; }
    }
}