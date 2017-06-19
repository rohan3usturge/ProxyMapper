namespace ProxyMapper.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProxyMapper.Util;

    internal sealed class DefaultValidatorChain<T> : IValidatorChain<T>
    {
        private readonly List<string> _errorList;
        private readonly List<IValidator<T>> _validatorSet;


        public DefaultValidatorChain(IValidator<T>[] validators)
        {
            validators.AssertNotNull("Validators' Array cannot be null");
            foreach (IValidator<T> validator in validators)
            {
                this.CheckValidatorBeforeModifyingSet(validator);
            }
            this._validatorSet = new List<IValidator<T>>(validators);
            this._errorList = new List<string>();
        }

        public IValidatorChain<T> AddToValidators(IValidator<T> validator)
        {
            this.CheckValidatorBeforeModifyingSet(validator);
            this._validatorSet.Add(validator);
            return this;
        }

        public IValidatorChain<T> RemoveFromValidators(IValidator<T> validator)
        {
            this.CheckValidatorBeforeModifyingSet(validator);
            this._validatorSet.Remove(validator);
            return this;
        }

        public IEnumerable<string> GetAllErrors()
        {
            return this._errorList;
        }

        public void ClearAllErrors()
        {
            this._errorList.Clear();
        }

        public IValidatorChain<T> Validate(T t)
        {
            if (t == null)
            {
                this._errorList.Add($"Please provide an Object of type {typeof(T)}");
                return this;
            }
            if (this._validatorSet == null || !this._validatorSet.Any())
            {
                return this;
            }
            foreach (IValidator<T> validator in this._validatorSet)
            {
                IEnumerable<string> collection = validator.Validate(t); 
                if (collection != null)
                {
                    this._errorList.AddRange(collection);
                }
            }
            return this;
        }

        public bool Success()
        {
            return !this._errorList.Any();
        }

        public bool Failure()
        {
            return this._errorList.Any();
        }


        private void CheckValidatorBeforeModifyingSet(IValidator<T> validator)
        {
            validator.AssertNotNull("Validator object cannot be null");
            if (this._validatorSet != null && this._validatorSet.Contains(validator))
            {
                throw new ArgumentNullException("Validator - [ " + validator.GetType().FullName +
                                                " ] already exists in validator chain.");
            }
        }
    }
}