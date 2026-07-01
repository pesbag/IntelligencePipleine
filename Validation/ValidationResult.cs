using IntelligencePipeline.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;
class ValidationResult
{
    private bool _isValid;
    private string _errorMessage;
    public bool IsValid {
        get { return _isValid; } }
    public string ErrorMessage {
        get { return _errorMessage; } }
    public ValidationResult (bool isValid, string errorMessage){}
    public static ValidationResult Success()
    {
        return new ValidationResult(true,string.Empty);
    }

    public static ValidationResult Failure(string errorMessage) {
        return new ValidationResult(false, errorMessage); ;
    }

}