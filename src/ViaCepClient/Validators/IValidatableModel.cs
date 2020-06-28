using System.Collections.Generic;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// IValidatableModel represents a model
    /// which can be validatable. It contains
    /// methods capable to check if a model is valid
    /// </summary>
    public interface IValidatableModel<TModel>
    {
        /// <summary>
        /// Checks if model is valid
        /// </summary>
        bool IsValid();

        /// <summary>
        /// Checks if model is invalid
        /// </summary>
        bool IsInvalid();

        /// <summary>
        /// Get validation errors
        /// </summary>
        IEnumerable<Errors> GetValidationErrors();
    }
}