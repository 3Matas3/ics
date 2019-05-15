using System;
using System.Linq.Expressions;

namespace SimpleTeamApp.App.UWPHelper.Validation
{
    public class ValidationRule
    {
        Expression<Func<bool>> _condition;
        string _message;

        public ValidationRule Condition(Expression<Func<bool>> condition)
        {
            _condition = condition;
            return this;
        }

        public ValidationRule Message(string message)
        {
            _message = message;
            return this;
        }

        public string Validate()
        {
            if (_condition == null)
                return string.Empty;
            else
                return _condition.Compile()() ? _message : string.Empty;
        }
    }
}
