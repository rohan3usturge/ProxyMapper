using System;
using System.Collections.Generic;
using ProxyMapper.Attributes;

namespace ProxyMapper.Validation
{
    internal class CallInfoValidator : IValidator<CallInfo>
    {
        public IEnumerable<string> Validate(CallInfo callInfo)
        {
            if (callInfo == null)
            {
                throw new ArgumentNullException(nameof(callInfo));
            }
            if (string.IsNullOrWhiteSpace(callInfo.QueryString))
            {
                throw new ArgumentNullException(nameof(callInfo.QueryString));
            }
            return new List<string>();
        }
    }
}