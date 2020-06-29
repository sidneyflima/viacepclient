using System.Collections.Generic;
using System.Linq;
using ViaCepClient.Validators.Internal;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// BaseValidatableModel represents a validatable model, in which
    /// is able to check if a model is valid
    /// </summary>
    public abstract class ValidatableModel<TModel>: IValidatableModel
    {
        /// <summary>
        /// Error collection
        /// </summary>
        private readonly List<IError> _errors;

        /// <summary>
        /// Get Rule Collection
        /// </summary>
        private readonly RuleSpecifications<TModel> _ruleSpecifications;

        /// <summary>
        /// Checks if has been validated
        /// </summary>
        private bool _hasBeenValidated;

        /// <summary>
        /// Get Rule Collection
        /// </summary>
        protected IRuleSpecifications<TModel> RuleSpecifications => _ruleSpecifications;

        /// <summary>
        /// BaseValidatableModel represents a validatable model, in which
        /// is able to check if a model is valid
        /// </summary>
        public ValidatableModel()
        {
            _ruleSpecifications = new RuleSpecifications<TModel>();
            _errors             = new List<IError>();
        }

        /// <summary>
        /// Checks if model is valid
        /// </summary>
        public bool IsValid()
        {
            ValidateIfHasNotBeenValidated();
            return !HasError();
        }

        /// <summary>
        /// Checks if model is invalid
        /// </summary>
        public bool IsInvalid()
        {
            ValidateIfHasNotBeenValidated();
            return HasError();
        }

        /// <summary>
        /// Get validation errors
        /// </summary>
        public IEnumerable<IError> GetValidationErrors()
        {
            ValidateIfHasNotBeenValidated();
            return _errors ?? Enumerable.Empty<IError>();
        }

        /// <summary>
        /// Validates a model
        /// </summary>
        public void Validate()
        {
            ClearErrorCollection();
            PerformValidation();
            MarkAsValidated();
        }

        /// <summary>
        /// Informs if model has been validated. If true and
        /// some properties has been changed, it is important
        /// to revalidate using method Validate() or use ReforceRevalidation()
        /// </summary>
        /// <returns></returns>
        public bool HasBeenValidated()
        {
            return _hasBeenValidated;
        }

        /// <summary>
        /// Used when any property has been changed, in order to inform
        /// that the model must be revalidated when check if has been valid
        /// </summary>
        protected void ReforceRevalidation()
        {
            _hasBeenValidated = false;
        }

        /// <summary>
        /// Adds an error
        /// </summary>
        protected void AddError(IError error)
        {
            _errors.Add(error);
        }

        /// <summary>
        /// Validate a model if has not been validated yet
        /// </summary>
        private void ValidateIfHasNotBeenValidated()
        {
            if (!_hasBeenValidated)
                Validate();
        }

        /// <summary>
        /// Returns if has error
        /// </summary>
        private bool HasError() 
        {
            return _errors?.Count > 0;
        }

        /// <summary>
        /// Mark as validated
        /// </summary>
        private void MarkAsValidated()
        {
            _hasBeenValidated = true;
        }

        /// <summary>
        /// Clear error collection
        /// </summary>
        private void ClearErrorCollection() 
        {
            _errors.Clear();
        }

        /// <summary>
        /// Performs model validation. A model is invalid if has any error
        /// after a validation. Therefore, if there is any invalid
        /// model property, the implementation must specify a error by
        /// adding a new error using 'AddError' method
        /// </summary>
        protected virtual void PerformValidation() 
        {
            // foreach(var propertySpecification in _ruleSpecifications.GetRuleSpecificationProperties())
            // {
            //     var propertyName = propertySpecification.PropertyName;
            //     foreach(var result in propertySpecification.ApplyRules())
            //     {
            //         AddError(new Error(result.ErrorCode, propertyName, result.ErrorMessage));
            //     }
            // }
        }
    }
}