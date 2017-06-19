namespace ProxyMapper.Validation
{
    using System.Collections.Generic;

    internal interface IValidatorChain<T>
    {
        IValidatorChain<T> Validate(T t);

        IValidatorChain<T> AddToValidators(IValidator<T> validator);

        IValidatorChain<T> RemoveFromValidators(IValidator<T> validator);

        IEnumerable<string> GetAllErrors();

        void ClearAllErrors();

        bool Success();

        bool Failure();
    }
}