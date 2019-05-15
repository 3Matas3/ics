using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace SimpleTeamApp.App.UWPHelper.Validation
{
    public interface ISupportValidation : IDataErrorInfo
    {
        ValidationRule AddValidationRule<PropertyT>(Expression<Func<PropertyT>> expression);
        void RemoveValidationRule<PropertyT>(Expression<Func<PropertyT>> expression);
        string Validate();
    }
}
