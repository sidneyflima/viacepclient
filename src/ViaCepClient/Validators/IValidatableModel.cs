using System.Collections.Generic;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// IValidatableModel represents a model
    /// which can be validatable. It contains
    /// methods capable to check if a model is valid
    /// </summary>
    public interface IValidatableModel
    {
        /// <summary>
        /// Validate a model. If has been validated, 
        /// it will reforce a new validation
        /// </summary>
        void Validate();

        /// <summary>
        /// Informs if model has been validated. If true and
        /// some properties has been changed, it is important
        /// to revalidate using method Validate()
        /// </summary>
        bool HasBeenValidated();

        /// <summary>
        /// Checks if model is valid. It will perform a validation
        /// if has not been validated yet
        /// </summary>
        bool IsValid();

        /// <summary>
        /// Checks if model is invalid. It will perform a validation
        /// if has not been validated yet
        /// </summary>
        bool IsInvalid();

        /// <summary>
        /// Get validation errors. It will perform a validation
        /// if has not been validated yet
        /// </summary>
        IEnumerable<IError> GetValidationErrors();
    }
}