namespace ProxyMapper.Validation
{
    using System.Collections.Generic;

    internal interface IValidator<in T>
    {
        IEnumerable<string> Validate(T t);
    }
}