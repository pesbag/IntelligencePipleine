using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Validation;
class SigalValidator : BaseValidator
{
    protected override ValidationResult ValidateSpecificFields(Report report)
    {
        if (report is not SignalReport signalReport)
        {
            return ValidationResult.Failure("Error: this report is not signal report");
        }
        if(signalReport.Frequency<1.0 || signalReport.Frequency > 3000.0)
        {
            return ValidationResult.Failure("Error: frequency report should be in range of 1.0 to 3000.0 {*}");
        }
        if (signalReport.Content.Length < 5 || signalReport.Content.Length > 1000)
        {
            return ValidationResult.Failure("Error: content report should be in range of 5 to 1000 characters {*}");
        }
        if (signalReport.SignalStrength < -120 || signalReport.SignalStrength > 0)
        {
            return ValidationResult.Failure("Error: signalStrength should be in range of -120 to 0 {*}");
        }
        
        bool isHebrew = string.Equals(signalReport.Language.ToString(), Language.Hebrew.ToString(), StringComparison.OrdinalIgnoreCase);
        bool isArabic = string.Equals(signalReport.Language.ToString(), Language.Arabic.ToString(), StringComparison.OrdinalIgnoreCase);
        bool isEnglish = string.Equals(signalReport.Language.ToString(), Language.English.ToString(), StringComparison.OrdinalIgnoreCase);
        bool isRussian = string.Equals(signalReport.Language.ToString(), Language.Russian.ToString(), StringComparison.OrdinalIgnoreCase);
        bool isOther = string.Equals(signalReport.Language.ToString(), Language.Other.ToString(), StringComparison.OrdinalIgnoreCase);
        
        if (!isHebrew && !isOther &&!isRussian && !isEnglish && !isArabic)
        {
            return ValidationResult.Failure($"Error: language report is illegal it should be one of {Language.Hebrew.ToString()}" +
                $",{Language.Arabic.ToString()},{Language.English.ToString()},{Language.Russian.ToString()},{Language.Other.ToString()}");
        }
        return ValidationResult.Success();
    }
}