namespace ViaCepClient.Validators
{
    /// <summary>
    /// IRule represents a rule specification used to validate a property value
    /// </summary>
    public interface IRule<TValue>
    {
        /// <summary>
        /// Apply rule specification
        /// </summary>
        IRuleResult ApplyRule(TValue value);
    }
}
